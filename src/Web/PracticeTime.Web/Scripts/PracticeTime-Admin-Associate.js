

$(document).ready(function () {

    getInstructorStudents();

    $("#submit").click(function() {
        getInstructorStudents();
    });

    $("#SelectedInstructor").change(function () {
        getInstructorStudents();
    });

    function getInstructorStudents() {
        var selectedInstructor = $("#SelectedInstructor").val();
        // Get student data
        $.ajax({
            url: "/Admin/GetInstructorStudents",
            dataType: 'json',
            async: true,
            type: "POST",
            data: { instructorId: selectedInstructor }
        }).success(function (data) {
            var jdata = $.parseJSON(data);
            addAssociations(jdata);
        });

    }

    function addAssociations(jdata) {
        $("#associations").empty();
        if (jdata.length == 0) {
            $("#associations").append('<li>' + 'No Students' + '</li>');
        } else {
            for (var row in jdata) {
                $("#associations").append('<li>' + jdata[row]["StudentName"] + '</li>');
            }
        }
    }
});
