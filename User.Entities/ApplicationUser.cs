namespace User.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public int VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public bool EmailNotifications { get; set; }
    }
}
