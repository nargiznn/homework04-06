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

        public static List<Group> groups = new List<Group>();
        public GroupsController()
        {
            groups = new List<Group>
            {
                new Group
                {
                    No = "PB301",
                    Id=1,
                    Limit = 10,
                    CreatedAt = DateTime.Now.AddYears(-1),
                    ModifiedAt = DateTime.Now.AddMonths(-2)
                },
                new Group
                {
                    No = "PB302",
                    Id=2,
                    Limit = 14,
                    CreatedAt = DateTime.Now.AddYears(-2),
                    ModifiedAt = DateTime.Now.AddMonths(-4)
                }
            };
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<GroupGetDto>>> GetAll()
        {
            if (_dbContext.Groups==null)
            {
                return NotFound();
            }
            var groups = await _dbContext.Groups.ToListAsync();
            var dtos = groups.Select(x => new GroupGetDto
            {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();
            return StatusCode(200,dtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GroupGetDto>> GetById(int id)
        {
            if (_dbContext.Groups == null)
            {
                return NotFound();
            }
            var group = await _dbContext.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            var dto = new GroupGetDto
            {
                Id = group.Id,
                No = group.No,
                Limit = group.Limit
            };
            return StatusCode(201, dto);
        }


        [HttpPost("")]
        public async Task<ActionResult<GroupGetDto>> Create([FromBody] GroupCreateDto createDto)
        {
            var newGroup = new Group
            {
                No = createDto.No,
                Limit = createDto.Limit,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _dbContext.Groups.Add(newGroup);
            await _dbContext.SaveChangesAsync();

            var dto = new GroupGetDto
            {
                Id = newGroup.Id,
                No = newGroup.No,
                Limit = newGroup.Limit
            };

            return CreatedAtAction(nameof(GetById), new { id = newGroup.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GroupGetDto>> Update(int id, [FromBody] GroupUpdateDto updateDto)
        {
            var group = await _dbContext.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            group.No = updateDto.No;
            group.Limit = updateDto.Limit;
            group.ModifiedAt = DateTime.Now;

            _dbContext.Groups.Update(group);
            await _dbContext.SaveChangesAsync();

            var dto = new GroupGetDto
            {
                Id = group.Id,
                No = group.No,
                Limit = group.Limit
            };

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var group = await _dbContext.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            _dbContext.Groups.Remove(group);
            await _dbContext.SaveChangesAsync();
            return NoContent();
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


