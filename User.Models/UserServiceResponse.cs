namespace User.Models
{
    public class UserServiceResponse
    {
        public bool IsValidUser { get; set; }

        public UserInfo User { get; set; }
    }
}
