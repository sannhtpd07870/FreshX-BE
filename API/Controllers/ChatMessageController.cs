using API.DTOs.Chat;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageController(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetAll()
        {
            var chatMessages = await _chatMessageService.GetAllAsync();
            return Ok(chatMessages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatMessageDto>> GetById(int id)
        {
            var chatMessage = await _chatMessageService.GetByIdAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }
            return Ok(chatMessage);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetByCustomerId(int customerId)
        {
            var chatMessages = await _chatMessageService.GetByCustomerIdAsync(customerId);
            return Ok(chatMessages);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetByEmployeeId(int employeeId)
        {
            var chatMessages = await _chatMessageService.GetByEmployeeIdAsync(employeeId);
            return Ok(chatMessages);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateChatMessageDto createDto)
        {
            var result = await _chatMessageService.CreateAsync(createDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _chatMessageService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }
    }
}
