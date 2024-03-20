using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhChat.Data;
using MinhChat.DbContext;
using MinhChat.Mapper;
using MinhChat.Model;
using System.Text.RegularExpressions;

namespace MinhChat.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly MinhChatDBContext _context;
        private readonly string[] AllowExtention;
        private readonly int FileSizeLimit;


        public UploadController(IWebHostEnvironment hostingEnvironment,
            IMapper mapper,
            MinhChatDBContext context,
            string[] allowExtention,
            IConfiguration con

            )
        {
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _context = context;
            FileSizeLimit = con.GetSection("FileUpload")
                .GetValue<int>("FilesizeLimit");
            AllowExtention = con.GetSection("FileUpload")
                .GetValue<string[]>("PermittedExtensions");

        }
        public async Task<IActionResult> Upload([FromForm] Model.UploadViewModel uploadViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Validate(uploadViewModel))
                {
                    return BadRequest("File is not valid");
                }
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" +
                    Path.GetFileName(uploadViewModel.File.FileName);
                var fouderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(fouderPath, fileName);
                if (!Directory.Exists(fouderPath))
                {
                    Directory.CreateDirectory(fouderPath);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadViewModel.File.CopyToAsync(fileStream);
                }
                var user = await _context.Users.
                    Where(x => x.UserName == User.Identity.Name)
                    .FirstOrDefaultAsync();
                var room = _context.Rooms.Where(u => u.Id == uploadViewModel.RoomId)
                    .FirstOrDefault();
                if (user == null || room == null)
                {
                    return BadRequest("User or Room is not valid");
                }
                string htmlImg = string.Format(
                    "<a href='/uploads/{0}' target='_blank'>" +
                    "<img src='/uploads/{0}' alt='image'" + "</a>", fileName);

                var message = new Message()
                {
                    RoomId = uploadViewModel.RoomId,
                    SenderId = user.Id,
                    Content = Regex.Replace(htmlImg, @"(?i)<?!img|a|/a|/img.*?>", string.Empty),
                    Timestamp = DateTime.Now
                };
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                var messageViewModel = _mapper.Map<Message, MessageViewModel>(message);
                return Ok(messageViewModel);
            }
            return Ok();

        }
        private bool Validate(Model.UploadViewModel uploadViewModel)
        {
            if (uploadViewModel.File == null)
            {
                return false;
            }
            if (uploadViewModel.File.Length == 0)
            {
                return false;
            }
            if (uploadViewModel.File.Length > FileSizeLimit)
            {
                return false;
            }
            if (!AllowExtention.Contains(Path.GetExtension(uploadViewModel.File.FileName)))
            {
                return false;
            }
            return true;
        }

    }
}
