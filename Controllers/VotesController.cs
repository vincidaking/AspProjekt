using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apka2.Data;
using Apka2.ViewModel;
using Apka2.Model;

namespace Apka2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly Context _context;

        public VotesController(Context context)
        {
            _context = context;
        }
     
        [HttpPost]
        public async Task<ActionResult<VoteFromReact>> PostVoteFromReact(VoteFromReact voteFromReact)
        {
            var lawOriginal = _context.Laws.FirstOrDefault(x => x.Id == voteFromReact.id);
            if (lawOriginal == null)
                return NotFound("Nie istnieje Law o podanym Id");

            var userOriginal = _context.Users.FirstOrDefault(x => x.Username == voteFromReact.username);
            if (lawOriginal == null)
                return NotFound("Nie isntnieje User o podanym Username");

            var alreadyVoted = _context.Votes.FirstOrDefault(x => x.User.Id == userOriginal.Id && x.Law == lawOriginal);
            if (alreadyVoted!=null)
                return Conflict("Użytkownik oddał już głos na tą ustawę");

            var temp = new Vote
            {
                Law = lawOriginal,
                User = userOriginal,
                VoteType = voteFromReact.voteTypeId
            };

            _context.Votes.Add(temp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoteFromReact", new { id = voteFromReact.id }, voteFromReact);
        }


       
    }
}
