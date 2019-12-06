/// <summary>
///  Author:    Tetrominoes Team
///  Date:      12/6/2019
///  Course:    CS 4540, University of Utah, School of Computing
 /// Copyright: CS 4540 and Tetrominoes Tesm - This work may not be copied for use in Academic Coursework.

 /// We, Tetrominoes Team, certify that we wrote this code from scratch and did not copy it in part or whole from
 /// another source.  Any references used in the completion of the assignment are cited in my README file.
   /// Purpose: The purpose of this document is to set handle player stat notes when they have been submitted
/// </summary>

//just meant to tell me a func loaded
$(function () {
    console.log("loaded");
});

//meant to submit a player stat
function submit_PlayerStatsNote(e, note_id, statID) {
    console.log("in submit_playerStatsNote function");
    console.log(e);
    e.preventDefault();

    $.ajax({
        url: "/Home/ChangeStatNote",
        method: "POST",
        data: { passednote: $('#note').val(), note_id: note_id, statID: statID },
    }).done(function (result) {
        console.log("action taken: " + result);
        $("#note").val(result.note);
    })
}
