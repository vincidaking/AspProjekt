using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apka2.Entities;
using Apka2.Helpers;
using Apka2.Model;
using Apka2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apka2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            

            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("registred")]
        public void Registred([FromBody]User userParam)
        {
            if(userParam.Username!=null && userParam.Password!=null)
            _userService.Register(userParam);
            else
                 BadRequest(new { message = "Username or password is empty" });

        }

        //[Authorize(Roles =Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]User userParam)
        {
            // map dto to entity and set id
            var user = userParam;
            user.Id = id;

            try
            {
                // save 
                _userService.Update(userParam);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        //[Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}