using GreenNote.Auth.Models.Dtos;

namespace GreenNote.Auth.Services.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<bool> AssignRole(string email,string roleName);
    }
}
