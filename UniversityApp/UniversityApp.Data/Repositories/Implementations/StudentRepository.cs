using System;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UniversityApp.Data.Repositories.Implementations
{
	public class StudentRepository: Repository<Student>, IStudentRepository
    {
        private readonly UniversityDbContext _context;
        public StudentRepository(UniversityDbContext context):base(context)
        {
            _context = context;
        }

    }
}

