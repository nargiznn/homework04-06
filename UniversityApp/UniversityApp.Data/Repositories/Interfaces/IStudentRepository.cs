using System;
using UniversityApi.Data.Entities;

namespace UniversityApp.Data.Repositories.Interfaces
{
	public interface IStudentRepository
	{
        void Add(Student entity);
        void Delete(Student entity);
        Student GetById(int id, bool? isDeleted = null, params string[] includes);
        List<Student> GetAll(bool? isDeleted = null, params string[] includes);
        int Save();
    }
}

