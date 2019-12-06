/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to specify the attributes of the stat's note model in the gamedata database
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
    /// a stat's note object :)
    /// </summary>
    public class StatNotes
    {
        //it has the following attributes
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
