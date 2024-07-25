namespace API.DTOs.Account
{
    public class AccountEmpDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}