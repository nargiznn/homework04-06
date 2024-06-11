using System;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Dtos.StudentDtos;

namespace University.Service.Interfaces
{
    public interface IStudentService
    {
        int Create(StudentCreateDto createDto);
        void Update(int id, StudentUpdateDto updateDto);
        StudentGetDto GetById(int id);
        List<StudentGetDto> GetAll(string? search = null);
        void Delete(int Id);
    }
}

