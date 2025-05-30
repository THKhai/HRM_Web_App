using Microsoft.AspNetCore.Authentication.JwtBearer;
using HRM_Web_App.Services;
using System.Text;
using HRM_Web_App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//(01) retrive the jwt information from the appsettings.json file
var jwtSetting = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSetting["Key"]);

//(6)Connect to PostgreSQL database
builder.Services.AddDbContext<AuthContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<NhanVienContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//(5)

// Configure the JWT token service
builder.Services.AddScoped<ITokenRepository, JWTRepository>();
// Add services to the container.
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthContext>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("User")
    .AddDefaultTokenProviders();

//(02)Configure Authentication with jwt
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//(4)Cấu hình yêu cầu mật khẩu cho tài khoản trong hệ thống
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true; 
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Password.GetHashCode();
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

//(3)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();