using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.ViewModel
{
    public class UserLawsVoted
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LawText { get; set; }

        public DateTime DateAdd { get; set; }

        public DateTime DateEnd { get; set; }

        public VoteType VoteType { get; set; }

    }
}
