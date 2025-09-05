namespace Application.Interfaces.Seeder
{
    public interface IPermissionDataSeeder
    {
        Task SeedPermissionsAsync();
        Task SeedRolePermissionsAsync();
    }
}
