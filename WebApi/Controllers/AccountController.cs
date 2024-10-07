using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Model.Entities;
using WebApi.Servises;

namespace WebApi.Controllers
{
   
    [ApiVersion("2")]
    [Route("api/v{version:apiversion}[controller]")]
    
    [ApiController]

    
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserRepository _userRepository;
        private readonly UserTokenRepository _userTokenRepository;

        public AccountController(IConfiguration config, UserRepository userRepository,UserTokenRepository userTokenRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
        }
        [HttpPost]
        public IActionResult post(string username, string password)
        {
            if (_userRepository.ValidateUser(username,password))
            {
                var user = _userRepository.GetUser(Guid.Parse("cfab6f58-5bfc-4888-b412-9b3d4b6cc476"));

                var claim = new List<Claim>
                {
                    new Claim("userId",user.Id.ToString()),
                    new Claim("Name",user.Name)

                };

                string key = _config["JwtConfig:key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenExp = DateTime.Now.AddMinutes(int.Parse(_config["JwtConfig:expires"]));


                var token = new JwtSecurityToken(
                    issuer: _config["JwtConfig:issuer"],
                    audience: _config["JwtConfig:audience"],
                    expires: tokenExp,
                    notBefore: DateTime.Now,
                    claims:claim,
                    signingCredentials:credentials
                    );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                _userTokenRepository.SaveToken(new UserTokenDto()
                {
                    MobileModel="A13 SumSung",
                      TokenExp  = tokenExp,
                      TokenHash=jwtToken,

                });

            return Ok(jwtToken);
        }
        }
    }
}
