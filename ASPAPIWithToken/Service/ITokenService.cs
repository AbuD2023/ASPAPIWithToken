namespace ASPAPIWithToken.Service
{
    public interface ITokenService
    {
        string GenerateToken(string name);
    }
}
