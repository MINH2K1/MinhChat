using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinhChat.Data
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        
        public int SenderId { get; set; }
        public User User { get; set; }


        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
