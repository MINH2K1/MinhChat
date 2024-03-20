using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinhChat.Data;

namespace MinhChat.DbContext
{
    public class MinhChatDBContext:IdentityDbContext<User,Role,int>
    { 
        public MinhChatDBContext(DbContextOptions<MinhChatDBContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }        
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
