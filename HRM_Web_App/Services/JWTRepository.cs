using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HRM_Web_App.Services;

public class JWTRepository : ITokenRepository
{
    private readonly IConfiguration configuration;
    public JWTRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string CreateToken(IdentityUser user, List<string> roles)
    {
        // Create claim
        var claim = new List<Claim>();
        claim.Add(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in roles) {
            claim.Add(new Claim(ClaimTypes.Role, role));
        }
        // create key
        var key = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        // create credentials
        var credentials = new SigningCredentials(key,
            SecurityAlgorithms.HmacSha256);
        // create token
        var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claim,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}