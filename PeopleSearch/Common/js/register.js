﻿$(document).ready(function () {
    console.log("ready!");
    HideWarnings();
});


function ChangeAttachment(e) {
    var files = e.target.files;
    console.log(files);

    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
            }

            console.log("before ajax call");
            $.ajax({
                type: "POST",
                url: '/Person/UploadImage',
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    console.log(result);
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] == "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                }
            });
        } else {
            alert("This browser doesn't support HTML5 file uploads!");
        }
    }

}

function RegisterPerson() {
    HideWarnings();

    if (isPersonValid()) {       

        console.log("Register button clicked.")
        var gender = $('#gender:checked').val();

        var personViewModel = {
            FirstName: $('#firstName').val().trim(),
            LastName: $('#lastName').val().trim(),
            Address: $('#address').val().trim(),
            DateOfBirth: $('#dob').val(),
            Hobbies: $('#hobbies').val(),
            Image: '',
            Gender: gender
        };

        var hobbies = $('#hobbies').val();
        var dob = $('#dob').val();


        console.log(hobbies);
        console.log(dob);

        $.ajax({
            url: "/Person/Create",
            type: "POST",
            data: JSON.stringify(personViewModel),
            dataType: "json",
            traditional: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                console.log("Person saved successfully");
                $('#successMessage').show();
            },
            error: function (err) {
                console.log("Error occured while saving a person's record");
                $('#errorMessage').hide();
            }
        });
    } else {
        $('#validationMessage').show();
        $('#successMessage').hide();
        $('#errorMessage').hide();
    }

}

function isPersonValid() {
    return $('#firstName').val().trim().length > 0 &&
        $('#dob').val().length > 0 && 
         new Date($('#dob').val()) <  new Date();
}

function HideWarnings() {
    $('#validationMessage').hide();
    $('#successMessage').hide();
    $('#errorMessage').hide();
}
