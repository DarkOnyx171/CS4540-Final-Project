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
        public int NoteID { get; set; }
        public string Note { get; set; }
        public string UserName  { get; set; }
        public int Liked { get; set; } //maybe have a button for people to like buttons TODO
        [ScaffoldColumn(false)]
        public DateTime TimeModified { get; set; }
        public PlayerStats Stat { get; set; }
        public int StatID { get; set; }
    }
}
