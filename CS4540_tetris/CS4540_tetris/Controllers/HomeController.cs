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
        //TO establish a DB for this controller
        private readonly ScoreContext _scorecontext;
        private readonly UserContext _usercontext;

        public HomeController(ILogger<HomeController> logger, ScoreContext scorecontext)
        {
            _logger = logger;
            _scorecontext = scorecontext;
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
        /// <param name="statID">the learning objective it is tied to</param>
        /// <returns>JSON success</returns>
        [HttpPost]
        public JsonResult ChangeStatNote(string passednote, int note_id, int statID)
        {
            //if(_context.LO_Notes.Find(note_id) != null)
            var note_from_db = _scorecontext.StatNotes.Find(note_id);
            //no note exists
            if (note_from_db == null)
            {
                note_from_db = new StatNotes
                {
                    note = passednote,
                    StatID = statID, //what should this be TODO
                    Time_Modified = DateTime.Now,
                };
                _scorecontext.StatNotes.Add(note_from_db);
            }
            //deleting a note
            else if (passednote == null && note_from_db != null)
            {
                _scorecontext.StatNotes.Remove(note_from_db);
            }
            //updating a note
            else
            {
                note_from_db.note = passednote;
                note_from_db.Time_Modified = DateTime.Now;
            }
            _scorecontext.SaveChanges();

            return Json(new
            {
                success = true,
                note = passednote,
                id = note_from_db.StatID,
                statID = statID
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
