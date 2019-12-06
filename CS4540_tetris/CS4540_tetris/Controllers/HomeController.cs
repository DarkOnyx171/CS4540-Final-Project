/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is pass information to views and handle database queries
/// </summary>

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
    /// <summary>
    /// This controller is meant to handle all requests for our tetrominoes website :)
    /// </summary>
    public class HomeController : Controller
    {
        //setting up database contexts that we can use
        private readonly ILogger<HomeController> _logger;
        private readonly GameDataContext _gamedatacontext;
        private readonly UserContext _usercontext;

        //constructing those based off of injections
        public HomeController(ILogger<HomeController> logger, GameDataContext gamedatacontext, UserContext usercontext)
        {
            _logger = logger;
            _gamedatacontext = gamedatacontext;
            _usercontext = usercontext;
        }

        //accessible to everyone
        //this is the tutorial/overviewpage
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        
        [AllowAnonymous]
        //This is the single player page and is accessible by everyone
        public IActionResult Single()
        {
            return View();
        }

        [Authorize]
        //the is the highest scores page
        public IActionResult Scores()
        {
            //sort the scores so we can display
            return View(_gamedatacontext.Scores.OrderByDescending(hs => hs.Value).ToList());
        }

        [Authorize]
        //this is the stats page and is only accessibly by logged in users
        public IActionResult Stats()
        {
            //get everyone's stats and arrange them
            var stats = _gamedatacontext.PlayerStats.OrderBy(player => player.UserName).Include(c => c.Note)
                .AsNoTracking();
            return View(stats.ToList());
        }

        
        [AllowAnonymous]
        //this is the messages page and anyone can send messages even if they are not logged in
        public IActionResult Messages()
        {
            return View();
        }

        [Authorize]
        //this is the page used to have dual games and you have to be logged in to visit this page to track stats
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
                    TimeModified =time,
                };
                _gamedatacontext.StatNotes.Add(note_from_db);
            }
            //updating a note
            else
            {
                StatNotes note_from_db = _gamedatacontext.StatNotes.Where(o => o.NoteID == note_id).Single();
                note_from_db.Note = passednote;
                note_from_db.TimeModified =time;
                _gamedatacontext.Update(note_from_db);
            }
            _gamedatacontext.SaveChanges();

            if (note_id == 0)
                note_id = _gamedatacontext.StatNotes.Where(o => o.Note == passednote).Single().NoteID;

            return Json(new
            {
                success = true,
                note = passednote,
                id = note_id,
                time = time.ToString()
            });
        }

        /// <summary>
        /// meh... it was here
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
