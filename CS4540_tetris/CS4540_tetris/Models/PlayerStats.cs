/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to specify the attributes of the player stats model in the gamedata database
/// </summary>

using CS4540_tetris.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    /// <summary>
    /// player stats object :)
    /// </summary>
    public class PlayerStats
    {
        //it has the following attributes
        [Key]
        public int PlayerStatsID { get; set; }
        public string UserName { get; set; }
        public GameUser User { get; set; }
        public DateTime LastGameDate { get; set; }
        public TimeSpan TotalTimePlayed { get; set; }
        public TimeSpan LongestGame { get; set; }
        public int HighestScore { get; set; }
        public int GamesPlayed { get; set; }
        public StatNotes Note { get; set; }
    }
}
