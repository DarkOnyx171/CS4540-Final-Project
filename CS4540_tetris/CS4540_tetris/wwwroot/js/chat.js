/// <summary>
///     Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
 /// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

 /// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
 /// another source.  Any references used in the completion of the assignment are cited in my README file.
   /// Purpose: The purpose of this document is handle recieving and sending messages between members in a chat
//we planned to use this for matchmaking technology
/// </summary>

"use strict";

//setting up a connection
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//what to do when recieving a message
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

//below is for sending messages
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});