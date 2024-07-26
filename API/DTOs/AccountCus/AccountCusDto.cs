using API.DTOs.Customer;
namespace API.DTOs.AccountCus
{
    public class AccountCusDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CustomerId { get; set; }

    }
}
