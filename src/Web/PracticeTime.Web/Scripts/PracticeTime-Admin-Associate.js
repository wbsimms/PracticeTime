

$(document).ready(function() {

    getInstructorStudents();


    function deleteAssociation(studentId) {
        var idToDelete = studentId;
        var instructorId = $("#SelectedInstructor").val();
        $.ajax({
            url: "/Admin/DeleteAssociation",
            dataType: 'json',
            async: true,
            type: "POST",
            data: { studentId: idToDelete, instructorId: instructorId }
        }).success(function (data) {
            var jdata = $.parseJSON(data);
            addAssociations(jdata);
        });
    }

    $("#submit").click(function() {
        getInstructorStudents();
    });

    $("#SelectedInstructor").change(function () {
        getInstructorStudents();
    });

    function getInstructorStudents() {
        var selectedInstructor = $("#SelectedInstructor").val();
        var studentId;
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
                $("#associations").append('<li>' + jdata[row]["StudentName"] + ' <button data-studentid="' + jdata[row]["StudentId"] + '" type="submit" class="btn btn-default delete-student"><i class="glyphicon glyphicon-remove-sign"></i> Delete</button></li>');

            }
            $(".delete-student").click(function () {
                var studentIdToDelete = $(this).data("studentid");
                deleteAssociation(studentIdToDelete);
            });

        }
    }
});
