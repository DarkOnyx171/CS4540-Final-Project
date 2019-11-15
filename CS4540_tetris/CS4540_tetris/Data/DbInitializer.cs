using CS4540_tetris.Areas.Identity.Data;
using CS4540_tetris.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Data
{
    public class DbInitializer
    {
        public static void Initialize(ScoreContext scorecontext, UserContext usercontext, UserManager<GameUser> userManager)
        {
            scorecontext.Database.Migrate();
            usercontext.Database.Migrate();
            SeedUsers(userManager);
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
