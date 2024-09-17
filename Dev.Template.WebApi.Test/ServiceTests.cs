using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Dev.Template.DBContext;
using Genesis.Repositories;
using Dev.Template.Model.Entity;
using Genesis.Controllers.Extension;
using Dev.Template.Services;

public class ServiceTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly ServiceCollection _services;

    public ServiceTests()
    {
        _services = new ServiceCollection();

        _services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        _services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<AppDbContext>());

        _services.AddGenesisScoped();
        _services.AddScopedByConvention(typeof(TestService).Assembly);
        _serviceProvider = _services.BuildServiceProvider();
    }

    [Fact]
    public void Test_Repository_Uses_DbContext()
    {
        var repository = _serviceProvider.GetService<IRepositoriesBase<Count>>();

        Assert.NotNull(repository);

        var dbContextField = typeof(RepositoriesBase<Count>).GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var dbContext = dbContextField?.GetValue(repository);

        Assert.IsType<AppDbContext>(dbContext);
    }

    [Fact]
    public void Test_AllInterfacesAreScoped()
    {
        var scopedServices = _services
            .Where(service => service.Lifetime == ServiceLifetime.Scoped)
            .Select(service => service.ServiceType)
            .ToList();

        var interfaces = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsInterface)
            .ToList();

        foreach (var @interface in interfaces)
        {
            // Check if the interface is registered as a scoped service
            if (!scopedServices.Contains(@interface))
            {
                Assert.Fail($"Interface {@interface.Name} is not registered as a scoped service.");
            }
        }
    }
}
