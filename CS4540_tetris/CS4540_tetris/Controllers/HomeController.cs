using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CS4540_tetris.Models;
using Microsoft.AspNetCore.Authorization;
using CS4540_tetris.Data;
using CS4540_tetris.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace CS4540_tetris.Controllers
{
    //accessible to only logged in users unless overwritten for specific view

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ScoreContext _scorecontext;
        private readonly UserContext _usercontext;

        public HomeController(ILogger<HomeController> logger, ScoreContext scorecontext, UserContext usercontext)
        {
            _logger = logger;
            _scorecontext = scorecontext;
            _usercontext = usercontext;
        }

        //accessible to everyone
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Scores()
        {
            //sort the scores so we can display
            return View(_scorecontext.Scores.OrderByDescending(hs => hs.Value).ToList());
        }

        [Authorize]
        public IActionResult Stats()
        {
            //using (var context = new ScoreContext())
            //{
            var stats = _scorecontext.PlayerStats.OrderBy(player => player.UserName).Include(c => c.Note)
                .AsNoTracking();
            return View(stats.ToList());
            //}
            //return View(_scorecontext.PlayerStats.OrderBy(player => player.UserName).ToList());
        }

        ////accessible to everyone
        [AllowAnonymous]
        public IActionResult Messages()
        {
            return View();
        }

        [Authorize]
        public IActionResult Dual()
        {
            ViewData["username"] = User.Identity.Name;
            return View(ViewData);
        }

        /// <summary>
        /// Designed to change the database when an stat note is submitted
        /// </summary>
        /// <param name="passednote">the note info</param>
        /// <param name="note_id">the note id</param>
        /// <param name="statID">the stat it is tied to</param>
        /// <returns>JSON success</returns>
        [HttpPost]
        public JsonResult ChangeStatNote(string passednote, int note_id, int statID)
        {
            DateTime time = DateTime.Now;
            //no note exists
            if (note_id == 0)
            {
                StatNotes note_from_db = new StatNotes
                {
                    Note = passednote,
                    StatID = statID,
                    TimeModified = time,
                };
                _scorecontext.StatNotes.Add(note_from_db);
            }
            //updating a note
            else
            {
                StatNotes note_from_db = _scorecontext.StatNotes.Where(o => o.NoteID == note_id).Single();
                note_from_db.Note = passednote;
                note_from_db.TimeModified = time;
                _scorecontext.Update(note_from_db);
            }
            _scorecontext.SaveChanges();

            if (note_id == 0)
                note_id = _scorecontext.StatNotes.Where(o => o.Note == passednote).Single().NoteID;

            return Json(new
            {
                success = true,
                note = passednote,
                id = note_id,
                time = time.ToString()
            });
        }

        [Route("SaveScore")]
        [HttpPost]
        [Authorize]
        public JsonResult SaveScore(int score, int duration, bool is_multi)
        {
            // Name, Score, Mode
            // Name, Score, Last Date, Total time, Longest time, Games Played.
            DateTime time = DateTime.Today;
            TimeSpan timePlayed = new TimeSpan(0, 0, duration / 1000);
            PlayerStats playerStats = _scorecontext.PlayerStats.Where(o => o.UserName == User.Identity.Name).FirstOrDefault();
            Score highScore = _scorecontext.Scores.Where(o => o.UserName == User.Identity.Name).FirstOrDefault();
            if (playerStats is null)
            {
                playerStats = new PlayerStats
                {
                    HighestScore = score,
                    LastGameDate = time,
                    LongestGame = timePlayed,
                    TotalTimePlayed = timePlayed,
                    UserName = User.Identity.Name,
                    GamesPlayed = 1
                };
                highScore = new Score
                {
                    GameMode = is_multi ? GameMode.Multi_Player : GameMode.Single_Player,
                    Nickname = User.Identity.Name,
                    Value = score,
                    UserName = User.Identity.Name
                };
                _scorecontext.PlayerStats.Add(playerStats);
                _scorecontext.Scores.Add(highScore);
            }
            else
            {
                playerStats.GamesPlayed++;
                playerStats.HighestScore = Math.Max(playerStats.HighestScore, score);
                playerStats.TotalTimePlayed += timePlayed;
                if (timePlayed > playerStats.LongestGame)
                {
                    playerStats.LongestGame = timePlayed;
                }
                playerStats.LastGameDate = time;

                if (highScore.Value < score)
                {
                    highScore.Value = score;
                    highScore.GameMode = is_multi ? GameMode.Multi_Player : GameMode.Single_Player;
                }
                _scorecontext.PlayerStats.Update(playerStats);
            }

            _scorecontext.SaveChanges();

            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
