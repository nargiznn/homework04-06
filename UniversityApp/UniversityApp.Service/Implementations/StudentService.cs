using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using University.Service.Interfaces;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Data.Repositories;
using UniversityApp.Data.Repositories.Interfaces;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Dtos.StudentDtos;
using UniversityApp.Service.Exceptions;

namespace UniversityApp.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository,IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        int IStudentService.Create(StudentCreateDto createDto)
        {
            if (_studentRepository.Exists(x => x.Email == createDto.Email && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "No", "No already taken email");
            }
            Student entity = new Student
            {
                GroupId=createDto.GroupId,
                FullName=createDto.FullName,
                Email=createDto.Email,
                BirthDate=createDto.BirthDate
            };
            _studentRepository.Add(entity);
            _studentRepository.Save();
            return entity.Id;

        }

        void IStudentService.Delete(int Id)
        {
            Student entity = _studentRepository.Get(x => x.Id == Id && !x.IsDeleted);
            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Student not found");
            entity.IsDeleted = true;
            entity.ModifiedAt = DateTime.Now;
            _studentRepository.Save();
        }

        List<StudentGetDto> IStudentService.GetAll(string? search)
        {
            return _studentRepository.GetAll(x => x.FullName.Contains(search)).Select(x => new StudentGetDto
            {
                Id = x.Id,
                Email=x.Email,
                FullName=x.FullName,

            }).ToList();
        }

        StudentGetDto IStudentService.GetById(int id)
        {
            Student entity = _studentRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (entity == null) throw new RestException(StatusCodes.Status404NotFound, "Student not found by given id");
            return _mapper.Map<StudentGetDto>(entity);
        }

        void IStudentService.Update(int id, StudentUpdateDto updateDto)
        {
            Student entity = _studentRepository.Get(x => x.Id == id && !x.IsDeleted);
            if (entity.Email != updateDto.Email && _studentRepository.Exists(x => x.Email == updateDto.Email && !x.IsDeleted))
            {
                throw new RestException(StatusCodes.Status400BadRequest, "No", "No already email taken");
            }
            entity.FullName = updateDto.FullName;
            entity.Email = updateDto.Email;
            entity.BirthDate = updateDto.BirthDate;
            entity.ModifiedAt = DateTime.Now;
            _studentRepository.Save();
        }
    }
}

