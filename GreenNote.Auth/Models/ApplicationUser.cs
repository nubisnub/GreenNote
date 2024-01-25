using Microsoft.AspNetCore.Identity;

namespace GreenNote.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        //개인회원 자료 조회용 prop
        public string StudentName { get; set; }

    }
}
