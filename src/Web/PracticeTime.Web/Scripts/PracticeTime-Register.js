
$(document).ready(function() {
    var value = $("#SelectedAccountType").val();

    $(".selectedAccountType").click(function()
    {
        var value = $(this).val();
        if (value == "Instructor") {
            $("#student-visibility-div").addClass("hidden");
        } else {
            $("#student-visibility-div").removeClass("hidden");
        }

    });

});