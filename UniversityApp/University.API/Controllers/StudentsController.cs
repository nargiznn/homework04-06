using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Service.Interfaces;
using UniversityApi.Data.Entities;
using UniversityApi.Models;
using UniversityApp.Controllers;
using UniversityApp.Data.Repositories.Interfaces;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApp.Service.Dtos.StudentDtos;
using UniversityApp.Service.Exceptions;
using UniversityApp.Service.Implementations;
using UniversityApp.Service.Interfaces;

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController: ControllerBase
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService,IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;

        }

        [HttpGet("")]
        public ActionResult<List<StudentGetDto>> GetAll()
        {
            return StatusCode(200, _studentService.GetAll());
        }


        [HttpGet("{id}")]
        public ActionResult<StudentGetDto> GetById(int id)
        {
            return Ok(_studentService.GetById(id));
              //return StatusCode(200, _studentService.GetById(id));
        }


        [HttpPost("")]
        public ActionResult Create(StudentCreateDto createDto)
        {

            return StatusCode(201, new { Id = _studentService.Create(createDto) });

            //Group group = _dbContext.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == createDto.GroupId && !x.IsDeleted);
            //if (group == null)
            //{
            //    ModelState.AddModelError("GroupId", "Group not found by give GroupId");
            //    return BadRequest(ModelState);
            //}
            //if (group.Limit <= group.Students.Count) return Conflict("Group is full");
            //if (_dbContext.Students.Any(x => x.Email.ToUpper() == createDto.Email.ToUpper() && !x.IsDeleted))
            //{
            //    ModelState.AddModelError("Email", "Email already exists");
            //    return BadRequest(ModelState);
            //}

            //Student entity = new Student
            //{
            //    FullName = createDto.FullName,
            //    Email = createDto.Email,
            //    BirthDate = createDto.BirthDate,
            //    GroupId = createDto.GroupId,
            //};

            //_dbContext.Students.Add(entity);
            //_dbContext.SaveChanges();

            //return Created(Request.Path, new { id = entity.Id });

        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, StudentUpdateDto updateDto)
        {
            _studentService.Update(id, updateDto);
            return NoContent();
            //var student = _dbContext.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            //if (student == null)
            //{
            //    return StatusCode(404);
            //}
            //if (student.Email != updateDto.Email && _dbContext.Students.Any(x => x.Email == updateDto.Email && !x.IsDeleted))
            //    return Conflict();


            //student.GroupId = updateDto.GroupId;
            //student.FullName = updateDto.FullName;
            //student.BirthDate = updateDto.BirthDate;
            //student.ModifiedAt = DateTime.Now;
            //_dbContext.SaveChangesAsync();
            //return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            _studentService.Delete(id);
            return NoContent();

            //var student = _dbContext.Students.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            //if (student == null)
            //{
            //    return NotFound();
            //}     
            //student.IsDeleted = true;
            //_dbContext.SaveChanges();
            //return NoContent();
        }


    }
}

