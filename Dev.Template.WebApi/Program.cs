using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Genesis.Repositories;
using Dev.Template.Repositories;
using Genesis.Controllers.Extension;
using System.Reflection;
using Genesis.Controllers.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionString:SqlServer"],
                         x =>
                         {
                             x.MigrationsAssembly("Dev.Template.Migrations.SqlServer");
                         }));

builder.Services.AddScoped<IDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

builder.Services.AddScopedByConvention(Assembly.GetExecutingAssembly());
builder.Services.AddGenesisScoped();

builder.Services.AddControllers();

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAllOrigins");

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
