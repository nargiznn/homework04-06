using System;
using AutoMapper;
using UniversityApi.Data.Entities;
using UniversityApp.Service.Dtos.GroupDtos;

namespace University.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Group, GroupGetDto>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
        }
    }
}
