using mvc.Interfaces;
using mvc.Models;
using mvc.DataContext;

namespace mvc.Services
{
    public class StudentModelService : IStudentModel
    {
        private List<StudentModel> listStudent = StudentDataContext.GetDataContextInstance().studentModel;
        public List<StudentModel> List()
        {
            return listStudent;
        }
        public StudentModel Create(StudentModel student)
        {
            var latestStudent = listStudent.OrderByDescending(x => x.Id).FirstOrDefault();
            student.Id = latestStudent is null ? 0 : latestStudent.Id + 1;
            try
            {
                listStudent.Add(student);
            }
            catch
            {
                throw new Exception("Can'not add  this student");
            }

            return student;
        }
        public StudentModel Update(StudentModel student)
        {
            var toUpdateStudent = listStudent.FirstOrDefault(x => x.Id == student.Id);
            if(toUpdateStudent is null) throw new Exception("This student doesn't exsit");

            toUpdateStudent.FirstName = student.FirstName;
            toUpdateStudent.LastName = student.LastName;
            toUpdateStudent.Gender = student.Gender;
            toUpdateStudent.DateOfBirth = student.DateOfBirth;
            toUpdateStudent.PhoneNumber = student.PhoneNumber;
            toUpdateStudent.BirthPlace = student.BirthPlace;
            toUpdateStudent.IsGraduated = student.IsGraduated;
            
            return student;
        }
        public StudentModel Delete(int id)
        {
            var toDeleteStudent = listStudent.FirstOrDefault(x => x.Id == id);
            if (toDeleteStudent is null)
            {
                throw new Exception("This student doesn't exist or someone deleted it");
            }

            //In production, data can be deleted in database or sth
            try
            {
                listStudent.Remove(toDeleteStudent);
            }
            catch (Exception except)
            {
                throw new Exception("Can not delete this student, please contact with IT");
            }

            return toDeleteStudent;
        }
        public StudentModel GetStudentById(int id)
        {
            return listStudent.FirstOrDefault(x=> x.Id  == id);
        }
    }
}