using API.DTOs.Customer;
namespace API.DTOs.AccountCus
{
    public class AccountDto
    {
        public int Id { get; set; } 
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public CustomerDto? Customer { get; set; }
        
    }
}
