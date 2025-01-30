using petchat.Models;

namespace petchat.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
