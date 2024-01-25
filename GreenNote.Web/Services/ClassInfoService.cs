using GreenNote.Web.Models;
using GreenNote.Web.Services.IServices;
using GreenNote.Web.Utility;

namespace GreenNote.Web.Services
{
    public class ClassInfoService : IClassInfoService
    {
        private readonly IBaseService _baseService;
        public ClassInfoService(IBaseService baseService)
        {
            _baseService= baseService;
        }

        public async Task<ResponseDto?> CreateAsync(ClassInfoDto classInfoDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data= classInfoDto,
                Url = SD.ClassInfoBase+ "/api/classinfo"

            });
        }

        public async Task<ResponseDto?> DeleteAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType= SD.ApiType.DELETE,             
                Url=SD.ClassInfoBase+"/api/ClassInfo/"+id
            });
        }

        public async Task<ResponseDto?> GetAllAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
              
                Url = SD.ClassInfoBase + "/api/classinfo"+"/GetAll"

            });
        }

        public async Task<ResponseDto?> GetStudentAsync(string Name)
        {
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,

				Url = SD.ClassInfoBase + "/api/classinfo/" +Name

			});
		}

        public async Task<ResponseDto?> GetOneDataAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ClassInfoBase + "/api/classinfo/" + id

            });
        }

        public async Task<ResponseDto?> UpdateAsync(ClassInfoDto classInfoDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = classInfoDto,
                Url = SD.ClassInfoBase+"/api/classinfo"
            });
        }
    }
}
