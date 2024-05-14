using Microsoft.AspNetCore.Identity;

namespace WiseBlogApp.API.Repositories.Interface;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}