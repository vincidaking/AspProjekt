using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apka2.Model;
using Apka2.Data;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Text;
using System.Net;

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



        //[Authorize(Roles = RoleNames.Admin)]
        [AllowAnonymous]

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Law>>> GetLaws()
        {

            return await _context.Laws.ToListAsync();
        }

        // GET: api/Laws/5
        [AllowAnonymous]
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

            if (law.DateEnd < DateTime.Today)
                return BadRequest("Data zakonczenia musi być pózniejsza");

            _context.Entry(law).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok(law);

        }

        // POST: api/Laws
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost]
        public async Task<ActionResult<Law>> PostLaw(Law law)
        {
            if (law.DateEnd < DateTime.Today)
                return BadRequest("Data zakonczenia musi być pózniejsza");


            var userToEmail = _context.Users.Select(x => x.Username).ToList();

            var temp = new Law
            {
                Name = law.Name,
                LawText = law.LawText,
                DateAdd = DateTime.UtcNow,
                DateEnd = law.DateEnd.AddDays(1).AddMilliseconds(-1)
            };


            _context.Laws.Add(temp);
            await _context.SaveChangesAsync();



            var credentials = new NetworkCredential("kamilonsz3@gmail.com", "euvictest");


            var mail = new MailMessage()
            {
                From = new MailAddress("kamilonsz3@gmail.com"),
                Subject = "Nowa Ustawa " + temp.Name,
                Body = temp.LawText+" Masz czas do "+temp.DateEnd+" by zagłosować"
            };


            foreach (var item in userToEmail)
            {
                mail.To.Add(new MailAddress($"{item}"));

            }

            // Smtp client
            var client = new System.Net.Mail.SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };

            // Send it...         
            client.Send(mail);






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
