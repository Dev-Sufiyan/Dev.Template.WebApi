using Dev.Template.Repositories;
using Dev.Template.Model.Entity;
using Dev.Template.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure database context
builder.Services.AddRepositories<AppRepositories>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionString:SqlServer"],
                         x =>
                         {
                             x.MigrationsAssembly("Dev.Template.Migrations.SqlServer");
                         }));

// Add Repositories services
builder.Services.AddScoped(typeof(IRepositoriesBase<>), typeof(RepositoriesBase<>));
builder.Services.AddScoped<ICountHistoryRepositories, CountHistoryRepositories>();

// Add controllers
builder.Services.AddControllers();

// Configure CORS to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // Set Swagger UI to be at /swagger/index.html
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Ensure CORS is used before routing
app.UseCors("AllowAllOrigins");

app.UseRouting();
app.UseAuthorization();

// Map controllers to endpoints
app.MapControllers();

app.Run();
