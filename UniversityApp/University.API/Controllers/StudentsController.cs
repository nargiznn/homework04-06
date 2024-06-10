using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Dtos.StudentDtos;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController: ControllerBase
    {
        private readonly UniversityDbContext _dbContext;

        public StudentsController(UniversityDbContext context)
        {
            _dbContext = context;
        }



        [HttpGet("")]
        public ActionResult<List<StudentGetDto>> GetAll()
        {
            List<StudentGetDto> dtos = _dbContext.Students.Where(x => !x.IsDeleted).Select(x => new StudentGetDto
            {
                Id = x.Id,
                FullName=x.FullName,
                Email=x.Email,
                BirthDate=x.BirthDate,
                GroupId=x.GroupId,
                Group=x.Group
            }).ToList();


            return StatusCode(200, dtos);
        }


        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> GetById(int id)
        {
            var data = _dbContext.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (data == null)
            {
                return StatusCode(404);
            }

            StudentGetDto dto = new StudentGetDto
            {
                Id = data.Id,
                FullName = data.FullName,
                Email = data.Email,
                BirthDate = data.BirthDate,
                GroupId = data.GroupId,

            };
            return StatusCode(200, dto);
        }




        [HttpPost("")]
        public ActionResult Create(StudentCreateDto createDto)
        {
            Group group = _dbContext.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == createDto.GroupId && !x.IsDeleted);
            if (group == null)
            {
                ModelState.AddModelError("GroupId", "Group not found by give GroupId");
                return BadRequest(ModelState);
            }
            if (group.Limit <= group.Students.Count) return Conflict("Group is full");
            if (_dbContext.Students.Any(x => x.Email.ToUpper() == createDto.Email.ToUpper() && !x.IsDeleted))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return BadRequest(ModelState);
            }

            Student entity = new Student
            {
                FullName = createDto.FullName,
                Email = createDto.Email,
                BirthDate = createDto.BirthDate,
                GroupId = createDto.GroupId,
            };

            _dbContext.Students.Add(entity);
            _dbContext.SaveChanges();

            return Created(Request.Path, new { id = entity.Id });

        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, StudentUpdateDto updateDto)
        {
            var student = _dbContext.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (student == null)
            {
                return StatusCode(404);
            }
            if (student.Email != updateDto.Email && _dbContext.Students.Any(x => x.Email == updateDto.Email && !x.IsDeleted))
                return Conflict();


            student.GroupId = updateDto.GroupId;
            student.FullName = updateDto.FullName;
            student.BirthDate = updateDto.BirthDate;
            student.ModifiedAt = DateTime.Now;
            _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
    
            var student = _dbContext.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (student == null)
            {
                return NotFound();
            }     
            student.IsDeleted = true;
            _dbContext.SaveChanges();
            return NoContent();
        }


    }
}

