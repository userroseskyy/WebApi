using WebApi.Context;
using WebApi.Model.Entities;

namespace WebApi.Servises
{
    public class UserTokenRepository
    {
        private readonly DataBaseContext _context;
        public UserTokenRepository(DataBaseContext context)
        {
            _context = context;
        }
        public void SaveToken(UserTokenDto userToken)
        {
            var usersTo=new UserToken()
            {
                Id = userToken.Id,
                TokenExp = userToken.TokenExp,
                TokenHash = userToken.TokenHash,
                User = userToken.User,
                UserId = userToken.UserId,
                MobileModel="A13"
               
            };
            _context.UserToken.Add(usersTo);
            _context.SaveChanges();

                
        }


    }


   

    

}
