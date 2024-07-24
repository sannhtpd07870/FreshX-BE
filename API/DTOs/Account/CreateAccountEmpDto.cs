namespace API.DTOs.Account
{
    public class CreateAccountEmpDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
    }
}