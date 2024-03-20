namespace MinhChat.Mapper
{
    public class UploadViewModel
    {
        public int Id { get; set; }
        
        public int RoomId { get; set; }
        public IFormFile File { get; set; }
    }
}
