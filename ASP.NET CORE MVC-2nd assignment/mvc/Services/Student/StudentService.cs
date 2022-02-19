using mvc.Interfaces;
using mvc.DataContext;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Services
{
    public class StudentService: Controller, IStudentService
    {
        private IStudentModel _studentModelService;
        public StudentService(IStudentModel studentModelService)
        {
            _studentModelService = studentModelService;
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var deletedStudent = _studentModelService.Delete(id);
                ViewData["NotificationType"] = 0;
                ViewData["Message"] = $"Deleted successfully student : {deletedStudent.GetFullName()}";
                string lastDeletedStudents = HttpContext.Session.GetString("info_deleted_students") ?? "" + "|" + deletedStudent.GetFullName(); 
                HttpContext.Session.SetString("info_deleted_students", lastDeletedStudents);
            }
            catch(Exception except)
            {
                ViewData["NotificationType"] = 1;
                ViewData["Message"] = except.Message;
            }       

            return View("Index",  _studentModelService.List());
        }
        public IActionResult Index()
        {
            return View(_studentModelService.List());
        }
        public IActionResult CreateOrUpdate(StudentModel student)
        {
            if (student.Id != 0)
            {
                try
                {
                    var updatedStudent = _studentModelService.Update(student);
                    ViewData["NotificationType"] = 0;
                    ViewData["Message"] = $"Successfully updated student's info: {updatedStudent.GetFullName()}";
                }
                catch(Exception exception)
                {
                    ViewData["NotificationType"] = 1;
                    ViewData["Message"] = exception.Message;
                }
            }
            else
            {
                try
                {
                   var createdStudent =  _studentModelService.Create(student);
                    ViewData["NotificationType"] = 0;
                    ViewData["Message"] = $"Successfully created a new student: {createdStudent.GetFullName()}";
                }
                catch(Exception except)
                {
                    ViewData["NotificationType"] = 1;
                    ViewData["Message"] = except.Message;
                }                
            }
            return View("Index", _studentModelService.List());
        }
        public List<StudentModel> List()
        {
            return _studentModelService.List();
        }
    }
}