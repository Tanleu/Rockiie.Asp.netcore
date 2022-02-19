using mvc.Models;
using mvc.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace mvc.ViewModels.StudentViewModel
{
    public class StudentViewModel
    {
        public StudentModel Student {get;set;}
        public List<SelectListItem> Genders {get;} = System.Enum.GetValues(typeof(Gender))
                        .Cast<Gender>()
                        .Select(x => new SelectListItem {Value =((int)x ).ToString(), Text = x.ToString() })
                        .ToList();

        public List<SelectListItem> GraduatedOptions {get;} = new List<SelectListItem>()
        {
            new SelectListItem {Value = "0", Text = "Under-graduated"},
            new SelectListItem {Value = "1", Text = "Graduated"}
        };
    }
}