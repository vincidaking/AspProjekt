using Apka2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.ViewModel
{
    public class HistoryResult
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string LawText { get; set; }      
        public DateTime DateEnd { get; set; }

        public int Accept { get; set; }
        public int Decline { get; set; }
        public int None { get; set; }
        public VoteType Winer { get; set; }


    }
}
