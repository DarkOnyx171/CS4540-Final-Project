using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    public class GameLog
    {
        [Key]
        public int GameID { get; set; }
        public GameMode Mode { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan GameTime { get; set; }
        public string Player { get; set; }
        public int Score { get; set; }
    }
}
