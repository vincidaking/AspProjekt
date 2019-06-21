using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Apka2.Model
{
    public class Law
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string LawText { get; set; }
        
        public DateTime DateAdd { get; set; }
       
        public DateTime DateEnd { get; set; }

        public ICollection<VoteType> VoteTypes { get; set; }
        public ICollection<LawUser> LawUsers { get; set; }
    }
}
