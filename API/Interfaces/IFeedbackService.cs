using API.DTOs.Feedback;
using API.DTOs.MedicalRecord;
using API.Models;

namespace API.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetAllFeedbacksAsync();
        Task<Feedback?> GetFeedbackByIdAsync(int FbackId);
        Task<Feedback?> UpdateFeedbackByIdAsync(int FbackId,UpdatingFeedbackDto updatingFeedbackDto);
        Task<Feedback> CreatingFeedbackAsync(AddingFeedback addingFeedback);
        Task<Feedback?> DeletingFeedbackByIdAsync(int FbackId);
    }
}
