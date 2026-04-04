using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartParkingApi.Data;
using SmartParkingApi.Models;
using SmartParkingApi.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SmartParkingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  private readonly IConfiguration _configuration;

  public AuthController(ApplicationDbContext context, IConfiguration configuration)
  {
    _context = context;
    _configuration = configuration;
  }

  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(UserDto request)
  {
    // Password encryption
    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

    var user = new User
    {
      Username = request.Username,
      PasswordHash = passwordHash,
      Role = "Staff"
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return Ok(user);
  }

  [HttpPost("login")]
  public async Task<ActionResult<string>> Login(UserDto request)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

    if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    {
      return BadRequest("Incorrect account or password");
    } 

    // If correct, create token
    string token = CreateToken(user);
    return Ok(new
    {
      message = "Login successfully",
      token = token,
      username = user.Username,
      role = user.Role
    });
  }

  private string CreateToken(User user)
  {
    List<Claim> claims = new List<Claim>
    {
      new Claim(ClaimTypes.Name, user.Username),
      new Claim(ClaimTypes.Role, user.Role)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
      _configuration.GetSection("AppSettings:Token").Value!));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.Now.AddDays(1),
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}