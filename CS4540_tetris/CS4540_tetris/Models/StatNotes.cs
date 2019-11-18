using CS4540_tetris.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    public class StatNotes
    {
        [Key]
        public int noteID { get; set; }
        public string note { get; set; }
        public string userName  { get; set; }
        public GameUser gameUser { get; set; }
        public int liked { get; set; } //maybe have a button for people to like buttons TODO
        [ScaffoldColumn(false)]
        public DateTime Time_Modified { get; set; }
    }
}
