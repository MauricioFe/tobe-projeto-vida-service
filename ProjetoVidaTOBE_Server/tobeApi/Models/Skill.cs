namespace tobeApi.Models
{
   public class Skill
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long ProfilesId { get; set; }
        public StudentProfile StudentProfile { get; set; }
    }
}
