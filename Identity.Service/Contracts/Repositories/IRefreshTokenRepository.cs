using Identity.Service.Models;

namespace Identity.Service.Contracts.Repositories;

public interface IRefreshTokenRepository
{
    public Task CreateRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);
    Task UpdateAsync(RefreshToken storedRefreshToken);
    Task DeleteAsync(string tokenId);
}