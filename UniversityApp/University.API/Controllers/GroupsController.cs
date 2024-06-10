using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApi.Data;
using UniversityApp.Service.Dtos.GroupDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Models;
using UniversityApi.Data;
using UniversityApp.Service.Dtos.GroupDtos;
using UniversityApi.Data.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly UniversityDbContext _dbContext;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(UniversityDbContext dbContext,ILogger<GroupsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetAll()
        {
            _logger.LogInformation("Group executing...");
            List<GroupGetDto> dtos = _dbContext.Groups.Where(x => !x.IsDeleted).Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();


            return StatusCode(200, dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            var data = _dbContext.Groups.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (data == null)
            {
                return StatusCode(404);
            }

            GroupGetDto dto = new GroupGetDto
            {
                Id = data.Id,
                No = data.No,
                Limit = data.Limit
            };
            return StatusCode(200, dto);
        }


        [HttpPost("")]
        public ActionResult Create(GroupCreateDto createDto)
        {
            if (_dbContext.Groups.Any(x => x.No == createDto.No && !x.IsDeleted))  return StatusCode(409);
            var entity = new Group
            {
                Limit = createDto.Limit,
                No = createDto.No
            };
            _dbContext.Groups.Add(entity);
            _dbContext.SaveChangesAsync();

            return StatusCode(201, new { Id = entity.Id });
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, GroupUpdateDto updateDto)
        {
            var group = _dbContext.Groups.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (group == null)
            {
                return StatusCode(404);
            }
            if (group.No != updateDto.No && _dbContext.Groups.Any(x => x.No == updateDto.No && !x.IsDeleted))
                return Conflict();


            group.No = updateDto.No;
            group.Limit = updateDto.Limit;
            group.ModifiedAt = DateTime.Now;
            _dbContext.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var group = _dbContext.Groups.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (group == null)
            {
                return StatusCode(404);
            }
            group.IsDeleted = true;
          _dbContext.SaveChanges();
            return NoContent();

        }

        
    }
}


