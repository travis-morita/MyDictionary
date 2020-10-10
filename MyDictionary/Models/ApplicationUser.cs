using Microsoft.AspNetCore.Identity;

namespace MyDictionary.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
