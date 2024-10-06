namespace WebApi.Model.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserToken> UserToken  { get; set; }

    }
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserToken> UserToken  { get; set; }

    }
}
