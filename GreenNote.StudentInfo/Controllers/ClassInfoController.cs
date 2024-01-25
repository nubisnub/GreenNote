using AutoMapper;
using GreenNote.StudentInfo.Data;
using GreenNote.StudentInfo.Models;
using GreenNote.StudentInfo.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenNote.StudentInfo.Controllers
{
    [Route("api/classinfo")]
    [ApiController]
    [Authorize]
    public class ClassInfoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private ResponseDto response;

        public ClassInfoController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            response = new();
        }

        [HttpGet]
        [Route("GetAll")]
        public ResponseDto GetAll()
        {
            try
            {
                IEnumerable<ClassInfo> allClassInfos = _appDbContext.ClassInfos.ToList();
                response.Result = _mapper.Map<IEnumerable<ClassInfoDto>>(allClassInfos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
		[Route("{studentName}")]
        public ResponseDto GetOne(string studentName)
        {
            try
            {
                IEnumerable<ClassInfo> OneStudentInfos = _appDbContext.ClassInfos.Where(o => o.Name == studentName);
                response.Result = _mapper.Map<IEnumerable<ClassInfoDto>>(OneStudentInfos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetById(int id)
        {
            try
            {
                ClassInfo? OneStudentInfos = _appDbContext.ClassInfos.FirstOrDefault(o => o.Id == id);
                response.Result = _mapper.Map<ClassInfoDto>(OneStudentInfos);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }



        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] ClassInfoDto classInfoDto)
        {
            try
            {
                ClassInfo obj = _mapper.Map<ClassInfo>(classInfoDto);
                _appDbContext.ClassInfos.Add(obj);
                _appDbContext.SaveChanges();

                response.Result = _mapper.Map<ClassInfoDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ClassInfoDto classInfoDto)
        {
            try
            {
                ClassInfo obj = _mapper.Map<ClassInfo>(classInfoDto);
                _appDbContext.ClassInfos.Update(obj);
                _appDbContext.SaveChanges();

                response.Result = _mapper.Map<ClassInfoDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpDelete]
		[Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                ClassInfo findObj = _appDbContext.ClassInfos.FirstOrDefault(u=>u.Id == id);
                _appDbContext.ClassInfos.Remove(findObj);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
