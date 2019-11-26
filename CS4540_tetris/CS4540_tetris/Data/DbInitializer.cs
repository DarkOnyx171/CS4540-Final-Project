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
        public static async Task InitializeAsync(ScoreContext gamedatacontext, UserContext usercontext, UserManager<GameUser> userManager)
        {
            gamedatacontext.Database.EnsureDeleted();
            usercontext.Database.EnsureDeleted();
            gamedatacontext.Database.Migrate();
            usercontext.Database.Migrate();
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
        public static void SeedNotes(ScoreContext notescontext)
        {
            // Look for any stats
            if (notescontext.PlayerStatNotes.Any())
            {
                return;   // DB has been seeded
            }
            //Initialize these scores
            var notes = new StatNotes[]
            {
            new StatNotes{
                gameUser = new GameUser
                {
                    UserName = "gamegorl@tetrominoes.com",
                    NickName = "Gorl",
                    Email = "gamegorl@tetrominoes.com",
                    EmailConfirmed = true
                },
                note = "Congrats on your great score!",
                Time_Modified = DateTime.Now,
                liked = 2,
                userName = "gameboi@tetrominoes.com",
            },
            new StatNotes{
                gameUser = new GameUser
                {
                    UserName = "gameboi@tetrominoes.com",
                    NickName = "Boi",
                    Email = "gameboi@tetrominoes.com",
                    EmailConfirmed = true
                },
                note = "Go back to space!",
                Time_Modified = DateTime.Now,
                liked = 100,
                userName = "gamealien@tetrominoes.com",
            },
            };
            foreach (StatNotes n in notes)
            {
                notescontext.PlayerStatNotes.Add(n);
            }

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
        public static void SeedPlayerStats(ScoreContext statscontext)
        {
            // Look for any stats
            if (statscontext.Scores.Any())
            {
                return;   // DB has been seeded
            }
            //Initialize these scores
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
                    UserName = "gamegorl@tetrominoes.com",
                    NickName = "Gorl",
                    Email = "gamegorl@tetrominoes.com",
                    EmailConfirmed = true
                },
                HighestScore = 2000,
                GamesPlayed = 1,
                LastGameDate = DateTime.Today,
                TotalTimePlayed = TimeSpan.FromMinutes(4),
                LongestGame = TimeSpan.FromMinutes(4),
                UserName = "gamegorl@tetrominoes.com",
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
            };
            foreach (PlayerStats s in stats)
            {
                statscontext.PlayerStats.Add(s);
            }

            try
            {
                statscontext.SaveChanges();
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
        public static void SeedScores(ScoreContext scorecontext)
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
                Value = 2000,
                Nickname = "Gorl",
                UserName = "gamegorl@tetrominoes.com",
                GameMode = GameMode.Multi_Player
            },
            new Score{
                Value = 25,
                Nickname = "Alien",
                UserName = "gamealien@tetrominoes.com",
                GameMode = GameMode.Multi_Player
            },
            };
            foreach (Score s in scores)
            {
                scorecontext.Scores.Add(s);
            }

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
        /// </summary>
        /// <param name="userManager"></param>
        public static async Task SeedUsers
            (UserManager<GameUser> userManager)
        {
            string password = "password";
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
