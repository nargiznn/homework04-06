using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Core.Entities;
using Course.Service.Dtos;
using Course.Service.Exceptions;
using Course.Service.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Course.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _groupService;

        public GroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("")]
        public ActionResult Create(GroupCreateDto createDto)
        {
            try
            {
                return StatusCode(201, new { id = _groupService.Create(createDto) });
            }
            catch (DublicateEntityException e)
            {
                return Conflict();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Error");
            }
        }


        //// GET: api/values
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

