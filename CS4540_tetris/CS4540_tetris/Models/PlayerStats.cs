using CS4540_tetris.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    public class PlayerStats
    {
        [Key]
        public int PlayerStatsID { get; set; }
        public string UserName { get; set; }
        public GameUser User { get; set; }
        public DateTime LastGameDate { get; set; }
        public TimeSpan TotalTimePlayed { get; set; }
        public TimeSpan LongestGame { get; set; }
        public int HighestScore { get; set; }
        public int GamesPlayed { get; set; }
    }
}
