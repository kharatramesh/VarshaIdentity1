using Microsoft.AspNetCore.Identity;

namespace VarshaIdentity1.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime WeddingDate { get; set; }
    }

}
