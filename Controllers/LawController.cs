using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Apka2.DAL;
using Apka2.Entities;
using Apka2.Helpers;
using Apka2.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Apka2.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LawController : ControllerBase
    {
        private ILaw _law;
        private readonly AppSettings _appSettings;

        public LawController(ILaw law, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            _law = law;
        }




        //[Authorize(Roles = Role.Admin)]
        [AllowAnonymous]
        [HttpPost("Add")]
        public void Add([FromBody]Law law)
        {
            if (law.LawText != null && law.DateEnd != null)
            {


                var temp = new Law
                {
                    Name=law.Name,
                    LawText = law.LawText,
                    DateEnd=law.DateEnd,
                    DateAdd=DateTime.Today
                    
                };
                _law.SaveLaw(temp);

            }
            else
                BadRequest(new { message = "Username or password is empty" });

        }

        //[Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
           
            var laws = _law.GetSavedLaws().ToList();
            return Ok(laws);
        }

        //[Authorize(Roles = Role.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Law law)
        {
            // map dto to entity and set id
            var temp = law;
            temp.Id = id;

            try
            {
                // save 
                _law.Update(law);
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
            _law.Delete(id);
            return Ok();
        }

    }
}