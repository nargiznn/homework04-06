using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApi.Data.Entities;
using UniversityApi.Dtos.GroupDtos;
using UniversityApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly UniversityDbContext _dbContext;

        public GroupsController(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public ActionResult<List<GroupGetDto>> GetAll()
        {
            List<GroupGetDto> dtos = _dbContext.Groups.Select(x => new GroupGetDto
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
            var data = _dbContext.Groups.FirstOrDefault(x => x.Id == id);

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
            var newGroup = new Group
            {
                No = createDto.No,
                Limit = createDto.Limit,
                CreatedAt = DateTime.Now,
             
            };

            _dbContext.Groups.Add(newGroup);
            _dbContext.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, GroupUpdateDto updateDto)
        {
            var group = _dbContext.Groups.Find(id);
            if (group == null)
            {
                return StatusCode(404);
            }

            group.No = updateDto.No;
            group.Limit = updateDto.Limit;
            group.ModifiedAt = DateTime.Now;

            _dbContext.Groups.Update(group);
            _dbContext.SaveChangesAsync();


            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var group = _dbContext.Groups.Find(id);
            if (group == null)
            {
                return StatusCode(404);
            }

            _dbContext.Groups.Remove(group);
          _dbContext.SaveChangesAsync();
             return StatusCode(200);
        }

        // GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //},
    }
}


