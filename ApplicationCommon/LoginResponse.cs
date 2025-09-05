namespace ApplicationCommon
{
    public record LoginResponse(bool Flag,
        string Message = null!,
        string Token = null!,
        string UserId = null!,
        string ServiceProviderId = null!);
}
