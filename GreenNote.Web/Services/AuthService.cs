using GreenNote.Web.Models;
using GreenNote.Web.Services.IServices;
using GreenNote.Web.Utility;

namespace GreenNote.Web.Services
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;

		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = registrationRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/AssignRole"
			});

		}

		public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = loginRequestDto,
				Url = SD.AuthAPIBase+"/api/auth/Login"
			});
		}

		public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.POST,
				Data = registrationRequestDto,
				Url = SD.AuthAPIBase + "/api/auth/Register"
			});
		}
	}
}
