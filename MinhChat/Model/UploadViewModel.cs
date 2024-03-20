using System.ComponentModel.DataAnnotations;

namespace MinhChat.Model
{
    public class UploadViewModel
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public int RoomId { get; set; }
    }
}
