using CS4540_tetris.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Data
{
    public class ScoreContext : DbContext
    {
        public ScoreContext(DbContextOptions<ScoreContext> options)
            : base(options)
        {
        }

        public DbSet<Score> Scores { get; set; }
    }
}
