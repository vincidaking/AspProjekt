using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Data.Vote
{
    public interface IVoteRepository
    {

        Task VoteSaveAsync(int idLaw, int idVoteType, string username);


    }
}
