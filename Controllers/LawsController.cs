﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apka2.Model;
using Apka2.Data;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Apka2.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LawsController : ControllerBase
    {
        private readonly Context _context;

        public LawsController(Context context)
        {
            _context = context;
        }

        // GET: api/Laws

        [HttpGet("withOptionVote/{username}")]
        public async Task<ActionResult<IEnumerable<Law>>> GetLawsWithVote(string username)
        {

            var userOrginal = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var LawsList = await _context.Laws.ToListAsync();

            //var ListWithOption = await _context.Votes.ToListAsync();



            var LawListWithOption = new List<Law>();

            foreach (var item in LawsList)
            {
                if (_context.Votes.FirstOrDefault(x => x.User.Id == userOrginal.Id && x.Law.Id == item.Id) == null)
                    LawListWithOption.Add(item);

            }


            var ListOut = new List<Law>();

            foreach (var item in LawListWithOption)
            {
                var temp = new Law
                {
                    Id = item.Id,
                    Name = item.Name,
                    LawText = item.LawText,
                    DateAdd = item.DateAdd,
                    DateEnd = item.DateEnd,
                    Votes = null
                };

                ListOut.Add(temp);

            }





            return ListOut.ToList();



        }

        [HttpGet("withoutOptionVote/{username}")]
        public async Task<ActionResult<IEnumerable<Law>>> GetLawsWithoutVote(string username)
        {

            var userOrginal = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var LawsList = await _context.Laws.ToListAsync();

            var ListWithoutOptions = _context.Votes.Where(x => x.User.Id == userOrginal.Id).ToList();

            var ListOut = new List<Law>();

            foreach (var item in ListWithoutOptions)
            {
                var temp = new Law
                {
                    Id = item.Law.Id,
                    Name = item.Law.Name,
                    LawText = item.Law.LawText,
                    DateAdd = item.Law.DateAdd,
                    DateEnd = item.Law.DateEnd,
                    Votes = null
                };

                ListOut.Add(temp);

            }



            return ListOut.ToList();
        }



       


        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Law>>> GetLaws()
        {

            return await _context.Laws.ToListAsync();
        }

        // GET: api/Laws/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Law>> GetLaw(int id)
        {
            var law = await _context.Laws.FindAsync(id);

            if (law == null)
            {
                return NotFound();
            }

            return law;
        }

        // PUT: api/Laws/5
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaw(int id, Law law)
        {

            if (id != law.Id)
                return BadRequest("Id muszę być takie same");

            var original = await _context.Laws.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (original == null)
                throw new Exception("Nie istnieje ustawa o takim Id");

            _context.Entry(law).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok(law);

        }

        // POST: api/Laws
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost]
        public async Task<ActionResult<Law>> PostLaw(Law law)
        {
            var temp = new Law
            {
                Name = law.Name,
                LawText = law.LawText,
                DateAdd = DateTime.Today,
                DateEnd = law.DateEnd
            };


            _context.Laws.Add(temp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaw", new { id = law.Id }, law);
        }


        [Authorize(Roles = RoleNames.Admin)]
        // DELETE: api/Laws/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Law>> DeleteLaw(int id)
        {
            var law = await _context.Laws.FindAsync(id);
            if (law == null)
            {
                return NotFound();
            }

            _context.Laws.Remove(law);
            await _context.SaveChangesAsync();

            return law;
        }

        private bool LawExists(int id)
        {
            return _context.Laws.Any(e => e.Id == id);
        }
    }
}
