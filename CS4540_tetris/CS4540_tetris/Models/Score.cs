using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    public enum GameMode
    {
        Single_Player, Multi_Player
    }

    public class Score
    {
        [Key]
        public int ScoreID { get; set; }
        public int Value { get; set; }
        public string Nickname { get; set; }
        public string UserName { get; set; }
        public GameMode GameMode { get; set; }
    }
}
