using Microsoft.AspNetCore.Mvc;
using API.Server.Interfaces;
using API.DTOs.Chat;

namespace API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSessionController : ControllerBase
    {
        private readonly IChatSessionService _chatSessionService;

        public ChatSessionController(IChatSessionService chatSessionService)
        {
            _chatSessionService = chatSessionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatSessionDto>>> GetAll()
        {
            var chatSessions = await _chatSessionService.GetAllAsync();
            return Ok(chatSessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatSessionDto>> GetById(int id)
        {
            var chatSession = await _chatSessionService.GetByIdAsync(id);
            if (chatSession == null)
            {
                return NotFound();
            }
            return Ok(chatSession);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateChatSessionDto createDto)
        {
            var result = await _chatSessionService.CreateAsync(createDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetById), new { id = createDto.CustomerId }, createDto);
        }

        [HttpPost("end/{id}")]
        public async Task<ActionResult> EndSession(int id)
        {
            var result = await _chatSessionService.EndSessionAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }
    }
}
