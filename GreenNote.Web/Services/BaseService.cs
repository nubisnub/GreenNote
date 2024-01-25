using GreenNote.Web.Models;
using GreenNote.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Unicode;
using static GreenNote.Web.Utility.SD;

namespace GreenNote.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("GreenNoteApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);



                if (requestDto.Data != null)
                {
					JsonSerializerSettings settings = new JsonSerializerSettings
					{
						DateFormatString = "yyyy-MM-dd",
						// 다른 필요한 설정들...
					};
                    string jsonData = JsonConvert.SerializeObject(requestDto.Data, settings);
					message.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponseMessage = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST: message.Method = HttpMethod.Post; break;
                    case ApiType.DELETE: message.Method = HttpMethod.Delete; break;
                    case ApiType.PUT: message.Method = HttpMethod.Put; break;
                    default: message.Method = HttpMethod.Get; break;
                }

                apiResponseMessage = await client.SendAsync(message);

                switch (apiResponseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
