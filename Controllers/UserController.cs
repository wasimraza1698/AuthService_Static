using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : Controller
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserController));
        private readonly IConfiguration _config;
        public readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo,IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }
        [HttpPost("Get")]
        public IActionResult Get(User valUser)
        {
            _log4net.Info("Trying to get User with UserName "+valUser.UserName);
            User user = _userRepo.Get(valUser);
            if (user != null)
            {
                _log4net.Info("Found User with Id "+user.UserID);
                return new OkObjectResult(user);
            }
            else
            {
                _log4net.Info("User not found with UserName"+valUser.UserName);
                return NotFound();
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(User user)
        {

            try
            {
                _log4net.Info("Authentication initiated with UserName " + user.UserName);
                IActionResult response;
                User valUser = _userRepo.Get(user);
                if (valUser == null)
                {
                    _log4net.Info("User not found");
                    return NotFound();
                }
                else
                {
                    _log4net.Info("Logging in user with id " + valUser.UserID.ToString());
                    var tokenString = GenerateJSONWebToken(valUser); 
                    response = Ok(new { token = tokenString });
                    return response;
                }
            }
            catch
            {
                return new NoContentResult();
            }
        }
        private string GenerateJSONWebToken(User user)
        {
            _log4net.Info("Token Generation initiated for UserId " + user.UserID.ToString());
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }
    }
}