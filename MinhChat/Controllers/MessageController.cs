using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhChat.Data;
using MinhChat.DbContext;
using MinhChat.Model;

namespace MinhChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly MinhChatDBContext _context;
        private readonly IMapper _mapper;
        public MessageController(MinhChatDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetMessages(int id)
        {
            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            var messsageViewModel = _mapper.Map<Message, MessageViewModel>(messages);
            return Ok(messsageViewModel);
        }
        [HttpGet("Room/{RoomName}")]
        public async Task<IActionResult> GetMessages(string RoomName)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Name == RoomName);
            if (room == null)
            {
                return BadRequest();
            }
            var messages = _context.Messages.Where(x => x.RoomId == room.Id)
                .Include(x => x.User)
                .Include(x => x.Room)
                .OrderByDescending(x => x.Timestamp)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();

            var messageViewModels = _mapper.Map<List<Message>, List<MessageViewModel>>(messages);
            return Ok(messageViewModels);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.Messages
                .Include(x => x.User)
                .Where(u => u.Id == id && u.User.UserName==User.Identity.Name)
                .FirstOrDefaultAsync();
            if (message == null)
            {
                return NotFound();
            }
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
