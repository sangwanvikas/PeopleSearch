
function SearchPerson(e) {
    var name = { 'name': $('#searchTextBox').val(), 'isExactMatchRequested': false };
    showProgress();
    $.ajax({
        url: "/Person/Result",
        type: "GET",
        data: name,
        success: function (msg) {
            hideProgress();
            $('#peopleDetails').html(msg);            
        },
        error: function (f) {
            console.log(f);
            hideProgress();
        }

    });
   
};

$("#searchTextBox").keyup(function (event) {
    if (event.keyCode == 13) {
        SearchPerson(event);
    }
});




// code for loader

var spinnerVisible = false;
function showProgress() {
    if (!spinnerVisible) {
        $("div#spinner").fadeIn("fast");
        spinnerVisible = true;
    }
};
function hideProgress() {
    if (spinnerVisible) {
        var spinner = $("div#spinner");
        spinner.stop();
        spinner.fadeOut("fast");
        spinnerVisible = false;
    }
};




