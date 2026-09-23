using Apps.Dtos.Users;
using Apps.Entities;
using Apps.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        public AuthController(UserService userService,JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IResult> Register(CreateUserDto request)
        {
           await _userService.StoreUser(request);
           return Results.Ok("User Create Successfully ");
        }

        [HttpPost("Login")]
        public async Task<IResult> Login (LoginUserDto request)
        {
            var user = await _userService.GetUserByEmail(request);
            if (user == null) {
                return Results.BadRequest("Invalid Email Address");
            }
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed){
                return Results.BadRequest("Wrong Password ");
            }

            var token = _jwtService.GenerateToken(
                  user.Id,
                  user.Email
              );

            return Results.Ok(new
            {
                user = user,
                token = token
            });

        }

    }
}
