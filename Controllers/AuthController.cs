//// Controllers/AuthController.cs
//using Microsoft.AspNetCore.Mvc;
//using MotorVault.Model.Domain;
//using MotorVault.Model.DTO;
//using MotorVault.Services;

//[ApiController]
//[Route("api/[controller]")]
//public class AuthController : ControllerBase
//{
//    private readonly ITokenService _tokenService;

//    // For demo, users in-memory. Replace with DB in real app.
//    private static List<User> users = new List<User>();

//    public AuthController(ITokenService tokenService)
//    {
//        _tokenService = tokenService;
//    }

//    [HttpPost("register")]
//    public IActionResult Register(UserDto userDto)
//    {
//        if (users.Any(u => u.Username == userDto.Username))
//            return BadRequest("User already exists.");

//        var user = new User
//        {
//            Username = userDto.Username,
//            Password = userDto.Password, // In production: hash passwords!
//            Role = "User" // Default role
//        };
//        users.Add(user);

//        return Ok("User registered.");
//    }

//    [HttpPost("login")]
//    public IActionResult Login(UserDto userDto)
//    {
//        var user = users.SingleOrDefault(u => u.Username == userDto.Username && u.Password == userDto.Password);
//        if (user == null)
//            return Unauthorized("Invalid credentials.");

//        var token = _tokenService.CreateToken(user);
//        return Ok(new { token });
//    }
//}
