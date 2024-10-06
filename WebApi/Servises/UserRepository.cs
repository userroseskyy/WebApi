using WebApi.Context;
using WebApi.Model.Entities;

namespace WebApi.Servises
{
    public class UserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context)
        {
            _context = context;

        }
        public User GetUser(Guid id) 
        {
           var user= _context.Users.SingleOrDefault(p => p.Id == id);
            return user;
        }
        public bool ValidateUser(string UserName,string Password)
        {
            var user = _context.Users.FirstOrDefault();
            return user !=null ?true : false;
        }

    }
}
