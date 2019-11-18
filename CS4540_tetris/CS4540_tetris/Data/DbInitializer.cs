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
        public static void Initialize(ScoreContext gamedatacontext, UserContext usercontext, UserManager<GameUser> userManager)
        {
            gamedatacontext.Database.Migrate();
            usercontext.Database.Migrate();
            SeedUsers(userManager);
            SeedScores(gamedatacontext);
            SeedPlayerStats(gamedatacontext);
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
                //TODO SAVE GAMEUSER
                HighestScore = 7000,
                GamesPlayed = 1,
                LastGameDate = DateTime.Today,
                TotalTimePlayed = TimeSpan.FromMinutes(12),
                LongestGame = TimeSpan.FromMinutes(12),
                UserName = "gameboi@tetrominoes.com",
            },
            new PlayerStats{
                //TODO SAVE GAMEUSER
                HighestScore = 2000,
                GamesPlayed = 1,
                LastGameDate = DateTime.Today,
                TotalTimePlayed = TimeSpan.FromMinutes(4),
                LongestGame = TimeSpan.FromMinutes(4),
                UserName = "gamegorl@tetrominoes.com",
            },
            new PlayerStats{
                //TODO SAVE GAMEUSER
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
        public static void SeedUsers
            (UserManager<GameUser> userManager)
        {
            if (userManager.FindByEmailAsync
                ("gameboi@tetrominoes.com").Result == null)
            {
                GameUser user = new GameUser();
                user.UserName = "gameboi@tetrominoes.com";
                user.NickName = "Boi";
                user.Email = "gameboi@tetrominoes.com";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync
                (user, "123ABC!@#def").Result;

                //NOT REALLY necessary since we only want one role player... we can just distinguish using logged in or nonlogged in user
                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user,
                //                        "Player").Wait();
                //}
            }
            if (userManager.FindByEmailAsync
                ("gamegorl@tetrominoes.com").Result == null)
            {
                GameUser user = new GameUser();
                user.UserName = "gamegorl@tetrominoes.com";
                user.NickName = "Gorl";
                user.Email = "gamegorl@tetrominoes.com";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync
                (user, "123ABC!@#def").Result;
            }
            if (userManager.FindByEmailAsync
                ("gamealien@tetrominoes.com").Result == null)
            {
                GameUser user = new GameUser();
                user.UserName = "gamealien@tetrominoes.com";
                user.Email = "gamealien@tetrominoes.com";
                user.NickName = "Alien";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync
                (user, "123ABC!@#def").Result;
            }
        }
    }
}
