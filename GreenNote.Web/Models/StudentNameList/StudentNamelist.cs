using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenNote.Web.Models.StudentNameList
{
    public class StudentNamelist
    {


        public StudentNamelist()
        {
            NameList = new List<SelectListItem>();
            
            Total = 3;

            NameList.Add(new SelectListItem { Text = "홍길동", Value = "홍길동" });
            NameList.Add(new SelectListItem { Text = "홍길순", Value = "홍길순" });
            NameList.Add(new SelectListItem { Text = "홍길자", Value = "홍길자" });
        }


        public List<SelectListItem> NameList { get; set; }
        public int Total { get; set; }

    }
}
