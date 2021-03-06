using mvc.Enum;
using mvc.Interfaces;

namespace mvc.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }

        public string GetGraduatedStringFormat() => IsGraduated ? "Graduated" : "Non-Graduated";
        public string GetFullName() => $"{FirstName}  {LastName}";
    }

}