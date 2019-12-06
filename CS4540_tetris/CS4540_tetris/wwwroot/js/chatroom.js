/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
 /// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

 /// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
 /// another source.  Any references used in the completion of the assignment are cited in my README file.
   /// Purpose: The purpose of this document is handle recieving and sending messages between members in a chat
//we planned to use this for matchmaking technology -- this is specifcially useful for match making in order to play dual
// this however will also handle messages being sent between the two
/// </summary>

"use strict";

//setting up a new connection with capabilities to play multiplayer
var connection = new signalR.HubConnectionBuilder().withUrl("/Multi_Player").build();
connection.start().then(function () {
    connection.invoke("GetOpenRooms").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

var roomid = "";
document.getElementById("sendButton").disabled = true;

//this is to start a new matchmaking or message room
document.getElementById("startRoomButton").addEventListener("click", function (event) {
    var room = document.getElementById("roomInput").value;
    roomid = room;
    connection.invoke("CreateRoom", room).catch(function (err) {
        return console.error(err.toString());
    });
});

//this is to join a room
document.getElementById("JoinButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var select = document.getElementById("RoomID");
    var room = select.options[select.selectedIndex].text;;

    roomid = room;

    connection.invoke("JoinRoom", user, room).catch(function (err) {
        return console.error(err.toString());
    });
});

//this is to add rooms to the list of rooms on the server
connection.on("SendRooms", function (rooms) {
    var roomselect = document.getElementById("RoomID");
    var roomlist = rooms.split("\n");
    roomlist.forEach(function (room) {
        var option = document.createElement("option");
        option.text = room;
        roomselect.add(option);
    })
})

//once a room has been started these buttons vhange
connection.on("StartRoom", function (id) {
    document.getElementById("JoinButton").disabled = true;
    document.getElementById("startRoomButton").disabled = true;
    document.getElementById("sendButton").disabled = false;
    roomid = id;
});

//inform that you have joined a room/chat
connection.on("JoinedRoom", function (user) {
    document.getElementById("JoinButton").disabled = true;
    document.getElementById("startRoomButton").disabled = true;
    document.getElementById("sendButton").disabled = false;

    var encodedMsg = user + " Joined Chat!";
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

//recieve a message
connection.on("ReceiveMessage", function (user, message)  {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    document.getElementById("messagesListDiv").scrollTop = document.getElementById("messagesListDiv").scrollHeight;
});

//send a message using the button
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message, roomid).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

//start a new game
function NewGame() {
    var room = document.getElementById("roomInput").value;
    roomid = room;
    connection.invoke("StartGame", roomid)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

//send game states to clients
function SendGameState() {
    var json = tetris.getTetrisJson();
    connection.invoke("SendGame", json, roomid)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

//inform the user they have lost
function SendGameover() {
    connection.invoke("Gameover", roomid)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

//this is a row in complete send it
function SendRow() {
    connection.invoke("Sendrow", roomid)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

//start a two player game obviously
connection.on("StartTwoPlayerGame", function (begin) {
    if (begin == true) {
        twoplayergame();
    }
});

//how to end game
connection.on("GameOver", function (end) {
    if (end == true) {
        resetgame();
    }
});

//this is saying a row is complete add it
connection.on("ReceiveRow", function (valid) {
    if (valid == true) {
        tetris.addBottomRow();
    }
});

//This is what happens when game is start
connection.on("ReceiveGame", function (json) {
    tetris.drawExternalBoard(json, 300);
});