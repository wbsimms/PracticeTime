

$(document).ready(function () {
    $("#SelectedStudent").change(function () {
        var selectedStudent = $("#SelectedStudent").val();

        // Get student data
        $.ajax({
            url: "/Instructor/GetSessionsForStudent",
            dataType: 'json',
            async: false,
            type: "POST",
            data: { studentId: selectedStudent }
        }).success(function (data) {
            var jdata = $.parseJSON(data);
        });

    });
});
