using Codepulse.API.Domain.Entities;
namespace Codepulse.API.Domain.Interfaces

{
    public interface ITokenRepository
    {
        string CreateJwtToken(AuthUser user, List<string> roles);
    }
}
