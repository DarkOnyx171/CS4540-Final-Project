/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to set up a game data database to hold scores, stats, notes
/// </summary>

using CS4540_tetris.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Data
{
    /// <summary>
    /// a class to hold all db tables and set up db
    /// </summary>
    public class GameDataContext : DbContext
    {
        public GameDataContext()
        {
        }

        public GameDataContext(DbContextOptions<GameDataContext> options)
            : base(options)
        {
        }

        //add each of these tables to game data
        public DbSet<Score> Scores { get; set; }
        public DbSet<GameLog> GameLogs { get; set; }
        public DbSet<MultiPlayerLog> MultiPlayerLogs { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<StatNotes> StatNotes { get; set; }
    }
}
