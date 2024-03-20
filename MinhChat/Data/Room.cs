using System.ComponentModel.DataAnnotations.Schema;

namespace MinhChat.Data
{
    public class Room
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public int AdminId { get; set;}
        
    }
}
