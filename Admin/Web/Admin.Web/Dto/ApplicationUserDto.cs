using Microsoft.AspNetCore.Identity;

namespace Admin.Web.Dto
{
    public class ApplicationUserDto : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
