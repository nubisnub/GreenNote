using System.ComponentModel.DataAnnotations;

namespace GreenNote.StudentInfo.Models
{
    public class ClassInfo
    {
        
        [Key]public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string ClassSubject { get; set; }
        [Required] public DateTime ClassDay { get; set; }
        [Required] public int ReviewRatio { get; set; }
        public string ClassContent { get; set; }
    }
}
