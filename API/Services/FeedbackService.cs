using API.DTOs.Feedback;
using API.DTOs.MedicalRecord;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FeedbackService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Feedback> CreatingFeedbackAsync(AddingFeedback addingFeedback)
        {
            var feedBack = _mapper.Map<Feedback>(addingFeedback);
            await _context.Feedback.AddAsync(feedBack);
            _context.SaveChanges();
            return feedBack;
        }

        public async Task<Feedback?> DeletingFeedbackByIdAsync(int FbackId)
        {
            var feedBack = await _context.Feedback.FirstOrDefaultAsync(o => o.Id == FbackId);
            if (feedBack == null)
            {
                return null;
            }
            _context.Feedback.Remove(feedBack);
            _context.SaveChanges();
            return feedBack;
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
           return await _context.Feedback.Include(f => f.AccountCus).ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int FbackId)
        {
            var feedBack = await _context.Feedback.Include(f => f.AccountCus).FirstOrDefaultAsync(o => o.Id == FbackId);
            return feedBack;
        }

        public async Task<Feedback?> UpdateFeedbackByIdAsync(int FbackId, UpdatingFeedbackDto updatingFeedbackDto)
        {
            var feedBack = await _context.Feedback.FirstOrDefaultAsync(o => o.Id == FbackId);
            if (feedBack == null) { return null; }
            feedBack.Comment = updatingFeedbackDto.Comment;
            feedBack.Status = updatingFeedbackDto.Status;
            feedBack.CreatedAt = updatingFeedbackDto.CreatedAt;
            feedBack.Rating = updatingFeedbackDto.Rating;
            await _context.SaveChangesAsync();
            feedBack = await _context.Feedback.Include(f => f.AccountCus).FirstOrDefaultAsync(o => o.Id == FbackId);
            return feedBack;
        }
    }
}
