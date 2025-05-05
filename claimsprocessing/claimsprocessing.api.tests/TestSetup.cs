using claimsprocessing.api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace claimsprocessing.api.tests
{
    internal class TestSetup : IDisposable
    {
        public ServiceProvider _serviceProvider = null!;
        public claims_processingContext _dbContext = null!;
        public IConfiguration _configuration = null!;

        public TestSetup(Action<IServiceCollection> registersServices, string connectionStringName = "DefaultConnection")
        {
            //Load appsettings.json
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            ServiceCollection services = new();

            //Get the connection string from appsettings.json
            //Register EF context with the connection string
            services.AddDbContext<claims_processingContext>(options =>
                options.UseSqlServer(connectionStringName));

            //Register services
            registersServices(services);

            //Build provider
            _serviceProvider = services.BuildServiceProvider();

            //Get the DB context
            _dbContext = _serviceProvider.GetRequiredService<claims_processingContext>();
            _dbContext.Database.EnsureCreated();
        }

        public T GetService<T>() where T: notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
