using Microsoft.AspNetCore.Mvc;
using mvc.DataContext;
using mvc.Enum;
using mvc.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using mvc.ViewModels.StudentViewModel;
using mvc.Interfaces;

namespace mvc.Controllers
{
    public class StudentController : Controller
    {
        //Dependency injection contructor
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
            listStudent = _studentService.List();
        }
        private List<StudentModel> listStudent;
        /// <summary>
        /// Main page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // string aString = HttpContext.Session.GetString("DeletedStudent");
            return _studentService.Index();
        }
        //For showing page CRUD
        public IActionResult Create()
        {
            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.Student = new StudentModel();
            return View("Display", studentViewModel);
        }
        public IActionResult Update(int id)
        {
            StudentModel student = listStudent.FirstOrDefault(x => x.Id == id) ?? new StudentModel();
            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.Student = student;
            ViewData["Message"] = "";
            return View("Display", studentViewModel);
        }
        public IActionResult Delete(int id)
        {
            var student = listStudent.FirstOrDefault(x => x.Id == id);
            return View("Delete", student);
        }

        //For actual action CRUD
        [HttpPost]
        public IActionResult UpdateStudent(StudentModel student)
        {
            return _studentService.CreateOrUpdate(student);
        }
        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            return _studentService.Delete(id);
        }
    }

    public class StudentExtensionController : Controller
    {
        private List<StudentModel> listStudent; 
        public StudentExtensionController(IStudentService studentService)
        {
            listStudent = studentService.List();
        }
        public IActionResult ListOfMaleMember()
        {
            return View("Views/Student/Index.cshtml", listStudent.Where(x => x.Gender == Enum.Gender.Male).ToList());
        }
        public IActionResult OldestStudent()
        {
            var oldestStudent = new List<StudentModel>()
            {
                listStudent.OrderByDescending(x => x.DateOfBirth.Subtract(DateTime.Now)).FirstOrDefault(),
            };

            return View("Views/Student/Index.cshtml", oldestStudent);
        }
        public IActionResult ListOfStudentWithFullNameOnly()
        {
            return View("Views/Student/Index.cshtml", listStudent);
        }
        public IActionResult ListOfStudentBasedOnAge(string comparableOperator, int comparableAge)
        {
            List<StudentModel> filteredStudentsByAge = new List<StudentModel>();
            if (comparableOperator == "=") filteredStudentsByAge = listStudent.Where(x => x.DateOfBirth.Year == comparableAge).ToList();
            else if (comparableOperator == "<") filteredStudentsByAge = listStudent.Where(x => x.DateOfBirth.Year < comparableAge).ToList();
            else filteredStudentsByAge = listStudent.Where(x => x.DateOfBirth.Year > comparableAge).ToList();

            return View("Views/Student/Index.cshtml", filteredStudentsByAge);
        }
        /// Using DOTNET.CORE NPOI
        /// </summary>
        /// <returns></returns>
        public FileResult GetDummyDataIntoExcelFile()
        {
            var exportedFile = "ListOfAllStudent.xlsx";
            var fs = new FileStream(exportedFile, FileMode.Create, FileAccess.Write);

            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("ListStudent");

            //Create header
            IRow headerRow = sheet1.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Last Name");
            headerRow.CreateCell(1).SetCellValue("First Name");
            headerRow.CreateCell(2).SetCellValue("Date Of Birth");
            headerRow.CreateCell(3).SetCellValue("Gender");
            headerRow.CreateCell(4).SetCellValue("Phone Number");
            headerRow.CreateCell(5).SetCellValue("Birth Place");
            headerRow.CreateCell(6).SetCellValue("Is Graduated");

            int i = 1;
            listStudent.ForEach(x =>
            {
                IRow detailRow = sheet1.CreateRow(i);
                detailRow.CreateCell(0).SetCellValue(x.LastName);
                detailRow.CreateCell(1).SetCellValue(x.FirstName);
                detailRow.CreateCell(2).SetCellValue(x.DateOfBirth.ToShortDateString());
                detailRow.CreateCell(3).SetCellValue(x.Gender.ToString());
                detailRow.CreateCell(4).SetCellValue(x.PhoneNumber);
                detailRow.CreateCell(5).SetCellValue(x.BirthPlace);
                detailRow.CreateCell(6).SetCellValue(x.GetGraduatedStringFormat());
                i++;
            });
            workbook.Write(fs);
            var downloadedExcelFile = System.IO.File.ReadAllBytes("./ListOfAllStudent.xlsx");
            return File(downloadedExcelFile, "application/vnd.ms-excel", "ListOfAllStudent.xlsx");
        }
    }
}
