"use strict";

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

document.getElementById("startRoomButton").addEventListener("click", function (event) {
    var room = document.getElementById("roomInput").value;
    roomid = room;
    connection.invoke("CreateRoom", room).catch(function (err) {
        return console.error(err.toString());
    });
});

document.getElementById("JoinButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var select = document.getElementById("RoomID");
    var room = select.options[select.selectedIndex].text;;

    roomid = room;

    connection.invoke("JoinRoom", user, room).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("SendRooms", function (rooms) {
    var roomselect = document.getElementById("RoomID");
    var roomlist = rooms.split("\n");
    roomlist.forEach(function (room) {
        var option = document.createElement("option");
        option.text = room;
        roomselect.add(option);
    })
})

connection.on("StartRoom", function (id) {
    document.getElementById("JoinButton").disabled = true;
    document.getElementById("startRoomButton").disabled = true;
    document.getElementById("sendButton").disabled = false;
    roomid = id;
});

connection.on("JoinedRoom", function (user) {
    document.getElementById("JoinButton").disabled = true;
    document.getElementById("startRoomButton").disabled = true;
    document.getElementById("sendButton").disabled = false;

    var encodedMsg = user + " Joined Chat!";
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    twoplayergame();
});

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
    document.getElementById("messagesListDiv").scrollTop = document.getElementById("messagesListDiv").scrollHeight;
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message, roomid).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


function InitTwoPlayerGame(event) {
    connection.invoke("StartGame", roomid)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

function NewGameRoom(event) {
    var room = document.getElementById("roomInput").value;
    roomid = room;
    connection.invoke("CreateRoom", room).catch(function (err) {
        return console.error(err.toString());
    });

    InitTwoPlayerGame();
}

function SendGameState() {
    json = tetris.getTetrisJson();
    connection.invoke("SendMessage", user, json, roomid)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

connection.on("ReceiveGame", function (json) {
    console.log(json);
});