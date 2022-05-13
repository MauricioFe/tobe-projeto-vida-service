using System.ComponentModel.DataAnnotations;

namespace tobeApi.Data.Dtos.Students
{
    public class StudentUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Role { get; set; }
    }
}
