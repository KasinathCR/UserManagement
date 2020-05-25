namespace User.Models
{
    public class UserInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }

        public string EmailAddress { get; set; }

        public bool EmailNotifications { get; set; }

        public bool IsVerified { get; set; }

        public string Address { get; set; }
    }
}