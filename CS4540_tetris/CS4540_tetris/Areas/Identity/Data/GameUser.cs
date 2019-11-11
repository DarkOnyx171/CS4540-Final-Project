using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CS4540_tetris.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the GameUser class
    public class GameUser : IdentityUser
    {
        override public string UserName { get; set; }
    }
}
