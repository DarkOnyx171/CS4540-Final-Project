using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS4540_tetris.Hubs
{
    public class ChatRoomHub : Hub
    {
        public async Task SendMessage(string user, string message, string group)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
        }

        public async Task CreateRoom(string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
            Program.rooms.Add(name, 1);

            await Clients.Caller.SendAsync("StartRoom", name);
        }

        public async Task JoinRoom(string user, string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            Program.rooms[group]++;

            await Clients.Group(group).SendAsync("JoinedRoom", user);
        }

        public async Task GetOpenRooms()
        {
            string result = "";

            foreach(string g in Program.rooms.Keys)
            {
                result += g + "\n";
            }

            await Clients.Caller.SendAsync("SendRooms", result);
        }

        public async Task StartGame(string group)
        {
            await Clients.Group(group).SendAsync("StartTwoPlayerGame");
        }

        public async Task SendGame(string group, string json)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", json);
        }
    }
}
