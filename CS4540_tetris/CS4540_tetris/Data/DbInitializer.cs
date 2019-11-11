using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Data
{
    public class DbInitializer
    {
        public static void Initialize(ScoreContext context)
        {
            context.Database.Migrate();
        }
    }
}
