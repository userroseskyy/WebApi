using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IActionResult post(string username, string password)
        {
            if (true)
            {
                var claim = new List<Claim>
                {
                    new Claim("userName",Guid.NewGuid().ToString()),
                    new Claim("Name","johndoe")

                };

                string key = "3b07ba42-9b82-43d9-b9a1-4e5256a7cdb5";
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



                var token = new JwtSecurityToken(
                    issuer: "shop35.ir",
                    audience: "shop35.ir",
                    expires: DateTime.Now.AddMinutes(5),
                    notBefore: DateTime.Now,
                    claims:claim,
                    signingCredentials:credentials
                    );}

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwtToken);
        }
    }
}
