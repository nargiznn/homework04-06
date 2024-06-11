using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using University.Service.Interfaces;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Data.Repositories;
using UniversityApp.Data.Repositories.Interfaces;
using UniversityApp.Service.Dtos.StudentDtos;
using UniversityApp.Service.Exceptions;

namespace UniversityApp.Service.Implementations
{
	public class StudentService: IStudentService
    {
        private readonly UniversityDbContext _context;
        private readonly IGroupRepository _groupRepository;
        public StudentService(UniversityDbContext context,IGroupRepository groupRepository)
		{
			_context = context;
            _groupRepository = groupRepository;
		}
		public int Create(StudentCreateDto createDto)
		{
            //Group group = _context.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == createDto.GroupId && !x.IsDeleted);
            Group group = _groupRepository.GetById(createDto.GroupId,false,"Students");

            if (group == null)
                throw new RestException(StatusCodes.Status404NotFound, "GroupId", "Group not found");

            if (group.Limit <= group.Students.Count)
                throw new RestException(StatusCodes.Status400BadRequest, "Group is full");

            if (_context.Students.Any(x => x.Email.ToUpper() == createDto.Email.ToUpper() && !x.IsDeleted))
                throw new RestException(StatusCodes.Status400BadRequest, "Email", "Student already exists");
            Student entity = new Student
            {
                FullName = createDto.FullName,
                Email = createDto.Email,
                BirthDate = createDto.BirthDate,
                GroupId = createDto.GroupId,
            };

            _context.Students.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }
    }
}

