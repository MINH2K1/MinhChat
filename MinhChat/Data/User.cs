
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhChat.Data
{
    public class User:IdentityUser<int>
    {

        public string ?Avatar { get; set; }
     
        
    }
}
