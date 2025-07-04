using HRM_Web_App.Data;
using HRM_Web_App.Models;
using HRM_Web_App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Web_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly NhanVienContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(NhanVienContext context ,UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        _context = context;
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }
    // POST: api/Auth/signup
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] RegisterRequestDto signUpRequest)
    {
        try
        {
            var identityUser = new IdentityUser
            {
                UserName = signUpRequest.UserName,
                Email = signUpRequest.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, signUpRequest.Password);
            if (identityResult.Succeeded)
            {
                // Add roles to user
                if (signUpRequest.Roles != null && signUpRequest.Roles.Any())
                {
                    identityResult =
                        await _userManager.AddToRolesAsync(identityUser, signUpRequest.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User created successfully");
                    }
                }
            }
            return BadRequest("User creation failed");
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message ?? e.Message);
        }
       
    }

    // POST: api/Auth/login
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (passwordCheck)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        // Generate JWT token
                        var token = _tokenRepository.CreateToken(user,roles.ToList());
                        return Ok(new { Token = token });
                    }
                }
            }

            return Unauthorized("Invalid credentials");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}