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

namespace CS4540_tetris.Controllers
{
    //accessible to only logged in users unless overwritten for specific view
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //TO establish a DB for this controller
        private readonly ScoreContext _scorecontext;

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

        public IActionResult Scores()
        {
            //sort the scores so we can display
            return View(_scorecontext.Scores.OrderBy(hs => hs.Value).ToList());
        }

        public IActionResult Stats()
        {
            return View();
        }

        ////accessible to everyone
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
