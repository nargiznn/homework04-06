using System;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Data.Repositories.Interfaces;

namespace UniversityApp.Data.Repositories.Implementations
{
	public class StudentRepository:IStudentRepository
	{
        private readonly UniversityDbContext _context;
        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }
        public Student? GetById(int id, bool? isDeleted = null, params string[] includes)
        {
            var query = _context.Students.AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            if (isDeleted != null) query = query.Where(x => x.IsDeleted == isDeleted);
            return query.FirstOrDefault(x => x.Id == id);
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
        }
        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }
        public List<Student> GetAll(bool? isDeleted = null, params string[] includes)
        {
            var query = _context.Students.AsQueryable();

            if (isDeleted != null) query = query.Where(x => x.IsDeleted == isDeleted);
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return query.ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}

