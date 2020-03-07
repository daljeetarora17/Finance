using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NDFinance.API.Entities;
using NDFinance.API.Helpers;
using NDFinance.API.Services;
using NDFinance.API.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NDFinance.API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public LoginController(
            IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        // POST: api/<controller>
        [HttpPost]
        public IActionResult Post(LoginVM userVM)
        {
            if (!string.IsNullOrEmpty(userVM.Username) && !string.IsNullOrEmpty(userVM.Password))
            {
                var user = _userService.Authenticate(userVM.Username, userVM.Password);

                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    // return basic user info and authentication token
                    return Ok(new
                    {
                        Id = user.Id,
                        Username = userVM.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = tokenString
                    });
                }
                else
                {
                    // return basic user info and authentication token
                    return Unauthorized();
                }

            }
            else
            {
                return BadRequest("Invalid Fields");
            }
        }

    }
}
