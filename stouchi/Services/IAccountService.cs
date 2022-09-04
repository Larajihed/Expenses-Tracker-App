using stouchi.Dtos;

namespace stouchi.Services
{
    public interface IAccountService
    {
        Task<TokenDto> GetAuthTokens(LoginDto login);
        Task<TokenDto> RenewTokens(RefreshTokenDto refreshToken);
    }
}
