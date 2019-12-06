/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to specify the attributes of the score and gamemode enum model in the gamedata database
/// </summary>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Models
{
    /// <summary>
    /// Game mode enum will keep track of what type of game that was played
    /// </summary>
    public enum GameMode
    {
        Single_Player, Multi_Player
    }

    /// <summary>
    /// score object :)
    /// </summary>
    public class Score
    {
        //it has the following attributes
        [Key]
        public int ScoreID { get; set; }
        public int Value { get; set; }
        public string Nickname { get; set; }
        public string UserName { get; set; }
        public GameMode GameMode { get; set; }
    }
}
