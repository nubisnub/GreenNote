using GreenNote.Auth.Data;
using GreenNote.Auth.Models;
using GreenNote.Auth.Models.Dtos;
using GreenNote.Auth.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace GreenNote.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext appDbContext, IJWTTokenGenerator jWTTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jWTTokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isCorrect = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || isCorrect == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);


            UserDto userDto = new UserDto()
            {
                Name = user.Name,
                Email = user.Email,
                ID = user.Id,
                PhoneNumber = user.PhoneNumber,
                StudentName = user.StudentName                
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser applicationUser = new()
            {
                UserName = registrationRequestDto.Email,
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                StudentName = registrationRequestDto.StudentName

            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appDbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        StudentName= userToReturn.StudentName
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";

        }
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
