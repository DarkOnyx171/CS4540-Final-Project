/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to initialize all the databases with something 
/// </summary>
using CS4540_tetris.Areas.Identity.Data;
using CS4540_tetris.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Data
{
    public class DbInitializer
    {
        /// <summary>
        /// this is to initialize the databases with data
        /// </summary>
        /// <param name="scorecontext"></param>
        /// <param name="usercontext"></param>
        /// <param name="userManager"></param>
        public static async Task InitializeAsync(GameDataContext gamedatacontext, UserContext usercontext, UserManager<GameUser> userManager)
        {
            //ensure the databases are deleted and the migrate them
            gamedatacontext.Database.EnsureDeleted();
            usercontext.Database.EnsureDeleted();
            gamedatacontext.Database.Migrate();
            usercontext.Database.Migrate();
            //seed the users, scores, stats, and statnotes
            await SeedUsers(userManager);
            SeedScores(gamedatacontext);
            SeedPlayerStats(gamedatacontext);
            SeedNotes(gamedatacontext);
        }

        
        /// <summary>
        /// this is to create player stat's notes if they do not exist
        /// </summary>
        /// <param name="scorecontext"></param>
        /// <param name="usercontext"></param>
        /// <param name="userManager"></param>
        public static void SeedNotes(GameDataContext notescontext)
        {
            // Look for any stats
            if (notescontext.StatNotes.Any())
            {
                return;   // DB has been seeded
            }
            //get these stats that we want to attach notes to
            PlayerStats firststat = notescontext.PlayerStats.Where(s => s.UserName == "gameboi@tetrominoes.com").ToList()[0];
            PlayerStats secondstat = notescontext.PlayerStats.Where(s => s.UserName == "gamealien@tetrominoes.com").ToList()[0];
            //Initialize these stat notes
            var notes = new StatNotes[]
            {
            new StatNotes{
                Note = "Congrats on your great score!",
                TimeModified = DateTime.Now,
                Liked = 2,
                UserName = "gameboi@tetrominoes.com",
                StatID = firststat.PlayerStatsID,
            },
            new StatNotes{
                Note = "Go back to space!",
                TimeModified = DateTime.Now,
                Liked = 100,
                UserName = "gamealien@tetrominoes.com",
                StatID = secondstat.PlayerStatsID,
            },
            };
            //add each note to the DB
            foreach (StatNotes n in notes)
            {
                notescontext.StatNotes.Add(n);
            }
            //try saving the db
            try
            {
                notescontext.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// this is to create player stats if they do not exist
        /// </summary>
        /// <param name="scorecontext"></param>
        /// <param name="usercontext"></param>
        /// <param name="userManager"></param>
        public static void SeedPlayerStats(GameDataContext scorecontext)
        {
            // Look for any stats
            if (scorecontext.PlayerStats.Any())
            {
                return;   // DB has been seeded
            }
            //Initialize these stats
            var stats = new PlayerStats[]
            {
            new PlayerStats{
                User = new GameUser
                {
                    UserName = "gameboi@tetrominoes.com",
                    NickName = "Boi",
                    Email = "gameboi@tetrominoes.com",
                    EmailConfirmed = true
                },
                HighestScore = 7000,
                GamesPlayed = 1,
                LastGameDate = DateTime.Today,
                TotalTimePlayed = TimeSpan.FromMinutes(12),
                LongestGame = TimeSpan.FromMinutes(12),
                UserName = "gameboi@tetrominoes.com",
            },
            new PlayerStats{
                User = new GameUser
                {
                    UserName = "gamealien@tetrominoes.com",
                    NickName = "Alien",
                    Email = "gamealien@tetrominoes.com",
                    EmailConfirmed = true
                },
                HighestScore = 25,
                GamesPlayed = 1,
                LastGameDate = DateTime.Today,
                TotalTimePlayed = TimeSpan.FromMinutes(4),
                LongestGame = TimeSpan.FromMinutes(4),
                UserName = "gamealien@tetrominoes.com",
            },
            //add stats to db
            };
            foreach (PlayerStats s in stats)
            {
                scorecontext.PlayerStats.Add(s);
            }
            //try to save stats to db
            try
            {
                scorecontext.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// this is to create scores if they do not exist
        /// </summary>
        /// <param name="scorecontext"></param>
        /// <param name="usercontext"></param>
        /// <param name="userManager"></param>
        public static void SeedScores(GameDataContext scorecontext)
        {
            // Look for any scores
            if (scorecontext.Scores.Any())
            {
                return;   // DB has been seeded
            }
            //Initialize these scores
            var scores = new Score[]
            {
            new Score{
                Value = 7000,
                Nickname = "Boi",
                UserName = "gameboi@tetrominoes.com",
                GameMode = GameMode.Single_Player
            },
            new Score{
                Value = 25,
                Nickname = "Alien",
                UserName = "gamealien@tetrominoes.com",
                GameMode = GameMode.Multi_Player
            },
            };
            //add each score to the db
            foreach (Score s in scores)
            {
                scorecontext.Scores.Add(s);
            }
            //try to save these to the db
            try
            {
                scorecontext.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// This is to create users if they do not yet exist
        /// 
        /// KEEP in mind we do not have roles because for the scope of this game 
        /// it would not make sense to have anything more than a player as a role
        /// </summary>
        /// <param name="userManager"></param>
        public static async Task SeedUsers
            (UserManager<GameUser> userManager)
        {
            //password string
            string password = "password";
            //if any of these users do not exist make them
            if (await userManager.FindByEmailAsync("gameboi@tetrominoes.com") == null)
            {
                GameUser user = new GameUser
                {
                    UserName = "gameboi@tetrominoes.com",
                    NickName = "Boi",
                    Email = "gameboi@tetrominoes.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, password);
            }
            if (await userManager.FindByEmailAsync("gamegorl@tetrominoes.com") == null)
            {
                GameUser user = new GameUser
                {
                    UserName = "gamegorl@tetrominoes.com",
                    NickName = "Gorl",
                    Email = "gamegorl@tetrominoes.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, password);
            }
            if (await userManager.FindByEmailAsync("gamealien@tetrominoes.com") == null)
            {
                GameUser user = new GameUser
                {
                    UserName = "gamealien@tetrominoes.com",
                    NickName = "Alien",
                    Email = "gamealien@tetrominoes.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, password);
            }
        }
    }
}
