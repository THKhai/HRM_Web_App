using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRM_Web_App.Data;

public class AuthContext:IdentityDbContext<IdentityUser,IdentityRole,string>
{
    public AuthContext (DbContextOptions<AuthContext> options) : base(options) {}
}