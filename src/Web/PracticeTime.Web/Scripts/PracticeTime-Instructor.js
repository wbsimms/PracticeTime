

$(document).ready(function() {
    getSessionsForStudent();


    $("#SelectedStudent").change(function() {
        getSessionsForStudent();
    });

    $("#delete-registration").click(function() {
        var studentToDelete = $("#SelectedRegisteredStudent").val();
        $.ajax({
            url: "/Instructor/RemoveRegistration",
            dataType: 'json',
            async: true,
            type: "POST",
            data: { studentId: studentToDelete }
        }).success(function (data) {
            $("#delete-text").text(data.DeleteResponse);
            $("#delete-message").removeClass("hidden");
            $("#SelectedRegisteredStudent option:selected").remove();
        });

    });

    function getSessionsForStudent() {
        var selectedStudent = $("#SelectedStudent").val();
        // Get student data
        $.ajax({
            url: "/Instructor/GetSessionsForStudent",
            dataType: 'json',
            async: true,
            type: "POST",
            data: { studentId: selectedStudent }
        }).success(function (data) {
            var jdata = $.parseJSON(data);
            appendSessionData(jdata);
        });
    }

    function appendSessionData(jdata) {
        var sessionData = $("#sessionData");
        if (jdata.length == 0) {
            sessionData.empty();
            sessionData.append('<tr><td>No Data</td><td/><td/><td/></tr>');
        } else {
            for (var row in jdata) {
                var date = new Date(Date.parse(jdata[0]['SessionDateTimeUtc']));
                var givenDate = $.datepicker.formatDate('mm/dd/yy', date);
                sessionData.append('<tr><td>' + jdata[0]['C_Instrument']['Name'] + '</td><td>' + jdata[0]['Title'] + '</td><td>' + givenDate + '</td><td>' + jdata[0]['Time'] + '</td></tr>');
            }

        }

    }


});
