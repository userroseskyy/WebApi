using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace WebApi.Servises.Validator
{
    public interface ITokenValidate
    {
        Task Execute(TokenValidatedContext context);
    }
    public class TokenValidate : ITokenValidate
    {
        private readonly UserRepository _userRepository;
        public TokenValidate(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(TokenValidatedContext context)

        {
            var claimsidentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsidentity.Claims==null|| !claimsidentity.Claims.Any())
            {
                context.Fail("Claims Not Found ...");
                return;
            }
            var userId = claimsidentity.FindFirst("userId").Value;

            if(!Guid.TryParse(userId,out Guid userGuid))
            {
                context.Fail("Claims Not Found ...");
                return;
            }
            var user = _userRepository.GetUser(userGuid);
            if (user.IsActive == false)
            {
                context.Fail("Claims Not Found ...");
                return;
            }   }
    }
}
