using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Controllers
{
   
    [ApiVersion("2")]
    [Route("api/v{version:apiversion}[controller]")]
    
    [ApiController]

    
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        public IActionResult post(string username, string password)
        {
            if (true)
            {
                var claim = new List<Claim>
                {
                    new Claim("userName",Guid.NewGuid().ToString()),
                    new Claim("Name","johndoe")

                };

                string key = _config["JwtConfig:key"];
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



                var token = new JwtSecurityToken(
                    issuer: _config["JwtConfig:issuer"],
                    audience: _config["JwtConfig:audience"],
                    expires: DateTime.Now.AddMinutes(int.Parse(_config["JwtConfig:expires"])),
                    notBefore: DateTime.Now,
                    claims:claim,
                    signingCredentials:credentials
                    );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwtToken);
        }
        }
    }
}
