using Microsoft.AspNetCore.Identity;

namespace UserApi.Models
{
    public class IdentityUserToBe : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
