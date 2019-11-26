///Author:    Jaecee Naylor
///Date:      10/18/2019
///Course:    CS 4540, University of Utah, School of Computing
///Copyright: CS 4540 and Jaecee Naylor - This work may not be copied for use in Academic Coursework.

///I, Jaecee Naylor, certify that I wrote this code from scratch and did not copy it in part or whole from
///another source.  Any references used in the completion of the assignment are cited in my README file.
///Purpose: This document is meant to hold all Ajax calls for anything related to a course or lo note
///

//just meant to tell me a func loaded
$(function () {
    console.log("loaded");
});

//meant to submit a player stat
function submit_PlayerStatsNote(e, note_id, statID) {
    console.log("in submit_LONoteProf function");
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
