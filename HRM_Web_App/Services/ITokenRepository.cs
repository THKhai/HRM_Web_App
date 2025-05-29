using Microsoft.AspNetCore.Identity;

namespace HRM_Web_App.Services;

public interface ITokenRepository
{
    string CreateToken(IdentityUser user, List<string> roles);
}