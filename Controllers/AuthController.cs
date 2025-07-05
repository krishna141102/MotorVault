// Controllers/AuthController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using MotorVault.Enum;
using MotorVault.Model.Domain;
using MotorVault.Model.DTO;
using MotorVault.Repository;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _usermanager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
    {
        _usermanager = userManager;
        _tokenRepository = tokenRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _usermanager.FindByNameAsync(userDto.Username);
        if (existingUser != null)
            return BadRequest("This username (email) is already taken.");

        var identityUser = new IdentityUser
        {
            UserName = userDto.Username,
            Email = userDto.Username
        };

        var createResult = await _usermanager.CreateAsync(identityUser, userDto.Password);

        if (!createResult.Succeeded)
        {
            if (createResult.Errors.Any(e => e.Code.Contains("Password")))
            {
                return BadRequest("Please create a password with a mix of uppercase, lowercase, digits, and special characters.");
            }

            return BadRequest(createResult.Errors.Select(e => e.Description));
        }

        // Add roles (if any)
        if (userDto.Roles != null && userDto.Roles.Any())
        {
            foreach (var role in userDto.Roles)
            {
                var roleResult = await _usermanager.AddToRoleAsync(identityUser, role);
                if (!roleResult.Succeeded)
                {
                    return BadRequest($"Failed to assign role '{role}'.");
                }
            }
        }

        return Ok("User registered successfully.");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _usermanager.FindByNameAsync(loginDto.Username);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        var result = await _usermanager.CheckPasswordAsync(user, loginDto.Password);
        if (result)
        {
            var userRoles = await _usermanager.GetRolesAsync(user);
            if (userRoles == null || !userRoles.Any())
            {
                return Unauthorized("User has no roles assigned.");
            }
            var jwtToken= _tokenRepository.CreateToken(user,userRoles.ToList());
            var response = new
            {
                Token = jwtToken,
            };
            return Ok(response);

        }
         return Unauthorized("Invalid username or password.");

        // Generate JWT token
        
    }
}
