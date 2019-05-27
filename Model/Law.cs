using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Apka2.Model
{
    public class Law
    {
        [Key]
        public int Id { get; set; }
        public string LawText { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAdd { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
               
    }
}
