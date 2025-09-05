namespace Infrastructure.Common
{
    public enum PermissionNames
    {
        Role_Read = 1,
        Role_Create = 2,
        Role_Update = 3,
        Role_Delete = 4,

        UserRole_Read = 5,
        UserRole_Create = 6,
        UserRole_Update = 7,
        UserRole_Delete = 8,
    }
    public static class CustomClaims
    {
        public const string Permissions = "permission";
    }
}
