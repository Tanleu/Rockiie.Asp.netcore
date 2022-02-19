using mvc.Models;

namespace mvc.Interfaces
{
    //Làm thế nào để không bị bó buộc vào Model truyền vào
    //Trong trường hợp có ParentStudentModel thì làm thế nào để không phải viết lại Interface??
    //VD:
    //Class cha Person?
    //Class con là : Student và ParentStudent?
    public interface IStudentModel
    {
        public List<StudentModel> List();
        public StudentModel Create(StudentModel student);
        public StudentModel Update(StudentModel student);
        public StudentModel Delete(int id);
        public StudentModel GetStudentById(int id);
    }
}