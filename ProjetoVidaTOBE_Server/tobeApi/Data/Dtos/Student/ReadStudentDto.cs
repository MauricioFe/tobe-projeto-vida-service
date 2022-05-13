namespace tobeApi.Data.Dtos.Student
{
    public class ReadStudentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Level { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
    }
}
