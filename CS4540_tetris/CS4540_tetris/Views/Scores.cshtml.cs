using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CS4540_tetris.Data;
using CS4540_tetris.Models;

namespace CS4540_tetris.Views
{
    public class ScoresModel : PageModel
    {
        private readonly CS4540_tetris.Data.GameDataContext _context;

        public ScoresModel(CS4540_tetris.Data.GameDataContext context)
        {
            _context = context;
        }

        public IList<Score> Score { get;set; }

        public async Task OnGetAsync()
        {
            Score = await _context.Scores.ToListAsync();
        }
    }
}
