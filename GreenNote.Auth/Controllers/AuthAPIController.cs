using Azure;
using GreenNote.Auth.Models.Dtos;
using GreenNote.Auth.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GreenNote.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var resultString = await _authService.Register(model);
            if (!string.IsNullOrEmpty(resultString))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = resultString;
                return BadRequest(resultString);
            }
            return Ok(_responseDto);         
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsygnc([FromBody] LoginRequestDto model)
        {
            var loginresponseDto = await _authService.Login(model);
            if (loginresponseDto.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginresponseDto;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Encountered";
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
