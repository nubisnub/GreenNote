using GreenNote.Web.Models;

namespace GreenNote.Web.Services.IServices
{
    public interface IClassInfoService
    {
        Task<ResponseDto?> GetAllAsync();
        Task<ResponseDto?> GetStudentAsync(string Name);
        Task<ResponseDto?> GetOneDataAsync(int id);
        Task<ResponseDto?> CreateAsync(ClassInfoDto model);
        Task<ResponseDto?> UpdateAsync(ClassInfoDto model);
        Task<ResponseDto?> DeleteAsync(int id);
    }
}
