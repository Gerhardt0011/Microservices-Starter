using Identity.Service.Contracts.Repositories;
using Identity.Service.Models;
using MongoDB.Driver;

namespace Identity.Service.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly IMongoCollection<RefreshToken> _refreshTokens;

    public RefreshTokenRepository(IMongoCollection<RefreshToken> refreshTokens)
    {
        _refreshTokens = refreshTokens;
    }

    public async Task CreateRefreshToken(RefreshToken refreshToken)
    {
        if(refreshToken == null)
        {
            throw new ArgumentNullException(nameof(refreshToken));
        }
        
        await _refreshTokens.InsertOneAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
    {
        return await _refreshTokens.Find(x => x.Id == refreshToken).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(RefreshToken storedRefreshToken)
    {
        await _refreshTokens.ReplaceOneAsync(x => x.Id == storedRefreshToken.Id, storedRefreshToken);
    }

    public async Task DeleteAsync(string tokenId)
    {
        await _refreshTokens.DeleteOneAsync(x => x.Id == tokenId);
    }
}