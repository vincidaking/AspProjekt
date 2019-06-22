using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apka2.Model;
using Microsoft.EntityFrameworkCore;

namespace Apka2.Data.Vote
{
    //public class VoteEFCoreRepository : IVoteRepository
    //{
    //    private Context _context;

    //    public VoteEFCoreRepository(Context context)
    //    {
    //        _context = context;
    //        _context.Database.EnsureCreatedAsync();
    //    }

    //    public async Task VoteSaveAsync(int idLaw, int idVoteType, string username)
    //    {
    //        //throw new NotImplementedException();

    //        var temp = _context.Votes.Add(new Model.Vote(
    //           {
    //            User = await _context.Users.FirstOrDefaultAsync(x => x.Username == username),
    //            Law = await _context.Laws.FirstOrDefaultAsync(x => x.Id == idLaw),
    //            VoteType = idVoteType
    //        }
            

    //            ));
            

    //    }

    //}
}

