using API.DTOs.Feedback;
using API.DTOs.MedicalRecord;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedBackService;
        private readonly IMapper _mapper;

        public FeedbacksController(IFeedbackService feedBackService, IMapper mapper)
        {
            this._feedBackService = feedBackService;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetFeedBacks()
        {
            var feedBacks = await _feedBackService.GetAllFeedbacksAsync();
            return Ok(_mapper.Map<List<FeedbackDto>>(feedBacks));
        }
        [HttpGet]
        [Route("{FBackId:int}")]
        public async Task<IActionResult> GetFeedbackById([FromRoute] int FBackId)
        {
            var feedBack = await _feedBackService.GetFeedbackByIdAsync(FBackId);
            if (feedBack == null)
            {
                return NotFound();
            }
            var feedBackDto = _mapper.Map<FeedbackDto>(feedBack);
            return Ok(feedBackDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreatingNewFeedback([FromBody] AddingFeedback addingFeedBack)
        {
            if (ModelState.IsValid)
            {
                var feedBack = await _feedBackService.CreatingFeedbackAsync(addingFeedBack);
                var feedBackDto = _mapper.Map<FeedbackDto>(feedBack);

                return CreatedAtAction(nameof(GetFeedbackById), new { FBackId = feedBack.Id }, feedBackDto);

            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("{ FBackId:int}")]
        public async Task<IActionResult> UpdatingFeedBackRecord([FromRoute] int  FBackId, [FromBody] UpdatingFeedbackDto updatingFeedback)
        {
            var feedBack = await _feedBackService.UpdateFeedbackByIdAsync(FBackId, updatingFeedback);
            if (feedBack == null)
            {
                return NotFound();
            }
            var feedBackDto = _mapper.Map<FeedbackDto>(feedBack);
            return Ok(feedBack);
        }
        [HttpDelete]
        [Route("{FBackId:int}")]
        public async Task<IActionResult> DeleteFeedBack([FromRoute] int FBackId)
        {
            var feedBack = await _feedBackService.DeletingFeedbackByIdAsync(FBackId);
            if (feedBack == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
