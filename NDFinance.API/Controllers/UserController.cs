using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDFinance.API.Entities;
using NDFinance.API.Models;
using NDFinance.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NDFinance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public List<User> Get()
        {
            return _userService.GetAll().ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]UserVM userVM)
        {
            try
            {
                _userService.CreateUser(userVM);

                return Ok(
                    new
                    {
                        Success = true,
                        Message = "Successfully Inserted."
                    });            
            }
            catch
            {
                return BadRequest(
               new
               {
                   Success = false,
                   Message = "Invalid Data."
               });
            }
        }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
}
