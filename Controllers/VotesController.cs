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
using Microsoft.AspNetCore.Authorization;

namespace Apka2.Controllers
{
    [Authorize]
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
                return BadRequest("Nie istnieje Law o podanym Id");

            var userOriginal = _context.Users.FirstOrDefault(x => x.Username == voteFromReact.username);
            if (lawOriginal == null)
                return BadRequest("Nie isntnieje User o podanym Username");

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
            return Ok();
            //return CreatedAtAction("GetVoteFromReact", new { id = voteFromReact.id }, voteFromReact);
        }


        [HttpGet("HistoryResult")]
        public async Task<ActionResult<IEnumerable<HistoryResult>>> HistoryResult()
        {

            var listResult = new List<HistoryResult>();
            //var listLaws = await _context.Laws.ToListAsync();
            var listLaws = await _context.Laws.Where(x=>x.DateEnd<= DateTime.Today).ToListAsync();
            //var voteList = await _context.Votes.ToArrayAsync();


            foreach (var item in listLaws)
            {
                var voteList =  _context.Votes.Where(x=>x.Law.Id==item.Id).ToList();

                var tempAccept = voteList.Where(x => x.VoteType == VoteType.Accept).Count();
                var tempDecline = voteList.Where(x => x.VoteType == VoteType.Decline).Count();
                var tempNone = voteList.Where(x => x.VoteType == VoteType.None).Count();

                var tempWiner = new VoteType();

                var max = new List<int> { tempAccept, tempDecline, tempNone }.Max();

                if (tempAccept == max && tempDecline == max) tempWiner = VoteType.None;
                else if (tempAccept == max) tempWiner = VoteType.Accept;
                else if (tempDecline == max) tempWiner = VoteType.Decline;
                else tempWiner = VoteType.None;



                var result = new HistoryResult
                {
                    Name=item.Name,
                    LawText=item.LawText,
                    DateEnd=item.DateEnd,
                    Accept=tempAccept,
                    Decline= tempDecline,
                    None = tempNone,
                    Winer = tempWiner
                };
                listResult.Add(result);
            }
           
           

         
            return listResult.ToList();
        }

        [HttpGet("withoutOptionVote/{username}")]
        public async Task<ActionResult<IEnumerable<UserLawsVoted>>> GetLawsWithoutVote(string username)
        {

            var userOrginal = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var LawsList = await _context.Laws.ToListAsync();

            var ListWithoutOptions = _context.Votes.Where(x => x.User.Id == userOrginal.Id).ToList();

            var ListOut = new List<UserLawsVoted>();

            foreach (var item in ListWithoutOptions)
            {
                var temp = new UserLawsVoted
                {
                    Id = item.Law.Id,
                    Name = item.Law.Name,
                    LawText = item.Law.LawText,
                    DateAdd = item.Law.DateAdd,
                    DateEnd = item.Law.DateEnd,
                    VoteType = item.VoteType
                };

                ListOut.Add(temp);

            }



            return ListOut.ToList();
        }

        [HttpGet("withOptionVote/{username}")]
        public async Task<ActionResult<IEnumerable<Law>>> GetLawsWithVote(string username)
        {

            var userOrginal = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            var LawsList = await _context.Laws.ToListAsync();

            //var ListWithOption = await _context.Votes.ToListAsync();
            //TODO dodanie viewmodelu z wybrtana opcja na ktora sie zaglosowalo


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

    }
}
