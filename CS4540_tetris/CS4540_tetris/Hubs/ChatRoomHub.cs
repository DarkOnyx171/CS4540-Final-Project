/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
/// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

/// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
/// another source.  Any references used in the completion of the assignment are cited in my README file.
/// Purpose: The purpose of this document is to 
/// </summary>
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Hubs
{
    /// <summary>
    /// this is used to send necessary thing to all clients in message rooms and create rooms and more
    /// </summary>
    public class ChatRoomHub : Hub
    {
        //used to send a message and tell all clients they need to receive the message
        public async Task SendMessage(string user, string message, string group)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
        }

        //this is used to create a message room and let other clients know that this room has been started
        //this way it can show up as a valid room
        public async Task CreateRoom(string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
            Program.rooms.Add(name, 1);

            await Clients.Caller.SendAsync("StartRoom", name);
        }

        //this is to add a client to a specific room
        public async Task JoinRoom(string user, string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            Program.rooms[group]++;

            await Clients.Group(group).SendAsync("JoinedRoom", user);
        }

        //this is to get a list of all open rooms so clients can see them
        public async Task GetOpenRooms()
        {
            string result = "";

            foreach(string g in Program.rooms.Keys)
            {
                result += g + "\n";
            }

            await Clients.Caller.SendAsync("SendRooms", result);
        }

        //this is to start a two player game in a matchmaking chat
        public async Task StartGame(string group)
        {
            await Clients.Group(group).SendAsync("StartTwoPlayerGame", true);
        }

        //this is to say game over when the lose
        public async Task Gameover(string group)
        {
            await Clients.Group(group).SendAsync("GameOver", true);
        }

        //this is to send rows between clients
        public async Task Sendrow(string group)
        {
            await Clients.OthersInGroup(group).SendAsync("ReceiveRow", true);
        }

        //this is to send the games to the clients
        public async Task SendGame(string json, string group)
        {
            await Clients.OthersInGroup(group).SendAsync("ReceiveGame", json);
        }
    }
}
