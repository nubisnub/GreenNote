using Newtonsoft.Json;

namespace GreenNote.Web.Models
{
    public class ClassInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassSubject { get; set; } = "수학2";

        [JsonProperty("classDay")]
        public DateTime ClassDay { get; set; }
        public int ReviewRatio { get; set; }
        public string ClassContent { get; set; } = "특이사항 없음";
    }
}
