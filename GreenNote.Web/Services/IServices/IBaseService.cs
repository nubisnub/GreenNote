using GreenNote.Web.Models;

namespace GreenNote.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer=true);

    }
}
