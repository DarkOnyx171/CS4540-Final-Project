/// <summary>
///  Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to add additional things necessary for user
/// </summary>

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
        public string NickName { get; set; }
    }
}
