namespace WebApi.Model.Entities
{
    public class UserToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public string MobileModel { get; set; }
        public User User{ get; set; }
        public Guid UserId{ get; set; }
    }
    public class UserTokenDto
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public string MobileModel { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
