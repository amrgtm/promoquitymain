namespace Application.DTOs.ApplicationUserRoleDTO
{
    public class UserRoleDTO
    {
        public Int64 Id { get; set; }   
       
        public Int64 RoleId { get; set; }
       
        public Int64 UserId { get; set; }
       
        public bool IsGranted { get; set; }
    }
}
