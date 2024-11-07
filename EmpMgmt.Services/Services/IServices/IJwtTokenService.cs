namespace EmpMgmt.Services.Services.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string userName);
    }
}
