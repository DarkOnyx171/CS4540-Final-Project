/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to specify the attributes of the game log model in the gamedata database
/// </summary>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    /// <summary>
    /// game log object :)
    /// </summary>
    public class GameLog
    {
        //it has the following attributes
        [Key]
        public int GameID { get; set; }
        public GameMode Mode { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan GameTime { get; set; }
        public string Player { get; set; }
        public int Score { get; set; }
    }
}
