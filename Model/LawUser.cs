using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Model
{
    public class LawUser
    {
        public int Id { get; set; }
        public int LawId { get; set; }
        public Law Law { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        
    }
}
