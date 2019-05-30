using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apka2.Data.Users;

using Apka2.Model;
using Apka2.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//TODO ogarnać automappera
//przerobić ILaw na ILawSErvice i ILawRepository

namespace Apka2.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersService _userService;

        public UsersController(IUsersRepository usersRepository, IUsersService userService)
        {
            _usersRepository = usersRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> GetAll()
        {
            var users = await _usersRepository.GetAllAsync();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> Authenticate([FromBody]User userParam)
        {
            var user = await _userService.AuthenticateAsync(userParam.Username, userParam.Password);
            return user;
        }

        [AllowAnonymous]
        [HttpPost("registred")]
        public async Task<User> Registred([FromBody]User userParam)
        {
            var registeredUser = await _userService.RegisterAsync(userParam);
            return registeredUser;

        }
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, [FromBody]User userParam)
        {
            // map dto to entity and set id
            var user = userParam;
            user.Id = id;

            try
            {
                // save 
                var updatedUser = await _usersRepository.UpdateAsync(userParam);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usersRepository.DeleteAsync(id);
            return Ok();
        }
    }
}