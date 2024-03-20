using Microsoft.AspNetCore.Identity;

namespace MinhChat.Data
{
    public class Role:IdentityRole<int>
    {
        public int ?RoleLevel { get; set; }
    }
}
