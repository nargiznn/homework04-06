using System;
using UniversityApi.Data.Entities;

namespace UniversityApp.Service.Dtos.StudentDtos
{
	public class StudentGetDto
	{
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Group Group { get; set; }
    }
}

