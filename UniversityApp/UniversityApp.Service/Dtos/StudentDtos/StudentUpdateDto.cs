using System;
using UniversityApi.Data.Entities;

namespace UniversityApp.Service.Dtos.StudentDtos
{
	public class StudentUpdateDto
	{

        public int GroupId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        //public Group Group { get; set; }

    }
}

