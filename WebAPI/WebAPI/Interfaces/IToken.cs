using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IToken
    {
        string CreateToken(AppUser user);
    }
}
