using APITutorials.Models;

namespace APITutorials.Services.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
