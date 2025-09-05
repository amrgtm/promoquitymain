using Application.Interfaces.Seeder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.SeedData
{
    public class SeedDataHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedDataHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleDataSeeder>();
            await roleSeeder.SeedRolesAsync();

            var permissionSeeder = scope.ServiceProvider.GetRequiredService<IPermissionDataSeeder>();
            await permissionSeeder.SeedPermissionsAsync();

            var defaultUserSeeder = scope.ServiceProvider.GetRequiredService<IUserDataSeeder>();
            await defaultUserSeeder.SeedDefaultUserAsync();

            var defaultUserRoleSeeder = scope.ServiceProvider.GetRequiredService<IUserRoleSeeder>();
            await defaultUserRoleSeeder.SeedDefaultUserRoleAsync();

            await permissionSeeder.SeedRolePermissionsAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
