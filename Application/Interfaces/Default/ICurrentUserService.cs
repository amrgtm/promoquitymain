namespace Application.Interfaces.Default
{
    public interface ICurrentUserService
    {
        long UserId { get; }
        long TenantId { get; }
    }
}
