using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    public class MultiPlayerLog
    {
        [Key]
        public int MultiPlayerLogID { get; set; }
        public int GameOneID { get; set; }
        public GameLog GameLogOne { get; set; }
        public int GameTwoID { get; set; }
        public GameLog GameLogTwo { get; set; }
    }
}
