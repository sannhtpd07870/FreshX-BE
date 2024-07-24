namespace API.DTOs.Account
{
    public class AccountEmpDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
    }
}