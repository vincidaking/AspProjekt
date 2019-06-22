using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.ViewModel
{
    public class VoteFromReact
    {
        public int id { get; set; }
        public VoteType voteTypeId { get; set; }
        public string username { get; set; }
    }
}
