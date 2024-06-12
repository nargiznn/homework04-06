using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Dtos.StudentDtos;

namespace UniversityApp.Service.Profiles
{
    public class AutoMapperProfile : Profile
    {
        private readonly IHttpContextAccessor _context;
        public AutoMapperProfile(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
            var uriBuilder = new UriBuilder(_context.HttpContext.Request.Scheme, _context.HttpContext.Request.Host.Host, _context.HttpContext.Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;
            //Groups
            CreateMap<Group, GroupGetDto>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
            //Students
            CreateMap<Student, StudentGetDto>()
                .ForMember(dest => dest.Age, s => s.MapFrom(s => DateTime.Today.Year - s.BirthDate.Year))
                .ForMember(dest => dest.ImageUrl, s => s.MapFrom(s => baseUrl + "uploads/students/" + s.FileName)); ;
        }
    }
}
