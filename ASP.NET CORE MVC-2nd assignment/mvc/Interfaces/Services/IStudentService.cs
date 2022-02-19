using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Interfaces
{
    public interface IStudentService {
        public IActionResult Index();
        public IActionResult CreateOrUpdate(StudentModel student);
        public IActionResult Delete(int id);
        public List<StudentModel> List();
    }
}