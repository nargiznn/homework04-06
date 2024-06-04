using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.Data.Entities;
using UniversityApi.Dtos.GroupDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityApi.Controllers
{
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {

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
        public ActionResult<List<GroupGetDto>> GetAll()
        {
            List<GroupGetDto> dtos = groups.Select(x => new GroupGetDto {
                Id = x.Id,
                No = x.No,
                Limit = x.Limit
            }).ToList();
            return StatusCode(200, dtos);
        }

        [HttpGet("{id}")]
        public ActionResult<GroupGetDto> GetById(int id)
        {
            var data = groups.Find(x => x.Id == id);
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
            return StatusCode(200,dto);
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
        //}
    }
}

