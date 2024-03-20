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
    public class RoomController : Controller
    {
        private readonly MinhChatDBContext _context;
        private readonly IMapper _mapper;
        public RoomController(MinhChatDBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<RoomViewModel>>> GetRoom()
        {

            var rooms = await _context.Rooms.ToListAsync();

            if (rooms == null)
            {
                return NotFound();
            }
            var roomViewModel = _mapper.Map<List<Room>, List<RoomViewModel>>(rooms);
            return Ok(roomViewModel);
        }
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            var roomViewModel = _mapper.Map<Room, RoomViewModel>(room);
            return Ok(roomViewModel);
        }
    }
}
