namespace ApplicationCommon.Enums
{
   
    public enum PermissionNames
    {
        //userread
        User_Read = 1,
        User_Create = 2,
        User_Update = 3,
        User_Delete = 4,

        Role_Read = 5,
        Role_Create = 6,
        Role_Update = 7,
        Role_Delete = 8,

        Permission_Read = 9,
        Permission_Create = 10,
        Permission_Update = 11,
        Permission_Delete = 12,

       

        UserRole_Read = 17,
        UserRole_Create = 18,
        UserRole_Update = 19,
        UserRole_Delete = 20,

       
    }
    public static class CustomClaims
    {
        public const string Permissions = "permission";
    }
}
