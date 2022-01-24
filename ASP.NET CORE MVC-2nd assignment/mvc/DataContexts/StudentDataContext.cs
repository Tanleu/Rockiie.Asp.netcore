using mvc.Models;
using mvc.Enum;

namespace mvc.DataContext
{
    public class StudentDataContext
    {
        private static StudentDataContext dataContext;
        public List<StudentModel> studentModel;
        public static StudentDataContext GetDataContextInstance()
        {
            if(dataContext is null) dataContext = new StudentDataContext();
            return dataContext;
        }
        private StudentDataContext()
        {
            studentModel = new List<StudentModel>();
            
            var initialListStudent = new List<StudentModel>()
            {
                new StudentModel()
                {
                    Id = 1,
                    FirstName = "Tan", 
                    LastName = "Leu Duy", 
                    Gender = Gender.Male, 
                    DateOfBirth = new DateTime(1996,09,11), 
                    PhoneNumber = "0922.11.09.96", 
                    BirthPlace = "Hải Dương", 
                    IsGraduated = true
                }
            ,
                new StudentModel()
                {
                    Id = 2,
                    FirstName = "Anh", 
                    LastName = "Nguyen Minh", 
                    Gender = Gender.Female, 
                    DateOfBirth = new DateTime(2000,07,01), 
                    PhoneNumber = "0922.92.41.23", 
                    BirthPlace = "Hải Dương", 
                    IsGraduated = false
                }
            ,
                new StudentModel()
                {
                    Id = 3,
                    FirstName = "Chinh", 
                    LastName = "Tran Van", 
                    Gender = Gender.Male, 
                    DateOfBirth = new DateTime(1988,02,28), 
                    PhoneNumber = "0942.12.09.16", 
                    BirthPlace = "Rân chơi hà lội", 
                    IsGraduated = true
                }
            ,
                new StudentModel()
                {
                    Id = 4,
                    FirstName = "Kien", 
                    LastName = "Vu Trung", 
                    Gender = Gender.Male, 
                    DateOfBirth = new DateTime(1988,02,28), 
                    PhoneNumber = "0988.23.11.96", 
                    BirthPlace = "Rân chơi hà thành", 
                    IsGraduated = true
                }
            ,
                new StudentModel()
                {
                    Id = 5,
                    FirstName = "Nghia", 
                    LastName = "Dao Van", 
                    Gender = Gender.Male, 
                    DateOfBirth = new DateTime(1988,02,28), 
                    PhoneNumber = "0999.99.03.94", 
                    BirthPlace = "Hà thành", 
                    IsGraduated = true
                }
            };
            studentModel.AddRange(initialListStudent);
        }
    }
}