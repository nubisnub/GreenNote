using AutoMapper;
using GreenNote.StudentInfo.Models;
using GreenNote.StudentInfo.Models.Dto;

namespace GreenNote.StudentInfo
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClassInfo, ClassInfoDto>();
                config.CreateMap<ClassInfoDto, ClassInfo>();
            });
            return mappingConfig;
        }
    }
}
