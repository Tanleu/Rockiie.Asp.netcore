using Microsoft.AspNetCore.Mvc;
using mvc.DataContext;
using mvc.Enum;
using mvc.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace mvc.Controllers
{
    public class StudentController: Controller
    {
        /// <summary>
        /// Main page.
        /// </summary>
        /// <returns></returns>
        private List<StudentModel> listStudent = StudentDataContext.GetDataContextInstance().studentModel; 
        public IActionResult Index()
        {
            return View(listStudent);
        }

        public IActionResult ListOfMaleMember()
        {
            return View("Index", listStudent.Where(x=> x.Gender == Enum.Gender.Male).ToList());
        }

        public IActionResult OldestStudent()
        {
            var oldestStudent = new List<StudentModel>()
            {
                listStudent.OrderByDescending(x => x.DateOfBirth.Subtract(DateTime.Now)).FirstOrDefault(),
            };

            return View("Index", oldestStudent);
        }

        public IActionResult ListOfStudentWithFullNameOnly()
        {
            return View("Index", listStudent);
        }

        public IActionResult ListOfStudentBasedOnAge(string comparableOperator,int comparableAge)
        {
            List<StudentModel> filteredStudentsByAge = new List<StudentModel>();
            if(comparableOperator == "=") filteredStudentsByAge = listStudent.Where(x=> x.DateOfBirth.Year == comparableAge).ToList();
            else if(comparableOperator == "<") filteredStudentsByAge = listStudent.Where(x=> x.DateOfBirth.Year < comparableAge).ToList();
            else filteredStudentsByAge = listStudent.Where(x=> x.DateOfBirth.Year > comparableAge).ToList();

            return View("Index", filteredStudentsByAge);
        }
        

        /// <summary>
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
            listStudent.ForEach(x => {
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

        public IActionResult CreateStudent()
        {
            return View("Display");
        }

        public IActionResult UpdateStudent(int studentId)
        {
            StudentModel student = StudentDataContext.GetDataContextInstance()
                                                    .studentModel.Where(x=> x.Id == studentId).FirstOrDefault();
            Console.Write(student.LastName);
            return View("Display", student);
        }

        public IActionResult DeleteStudent()
        {

            return View();
        }

        // public IActionResult DisplayDetailStudentInfo(StudentModel student)
        // {
        //     return View();    
        // }
    }
}