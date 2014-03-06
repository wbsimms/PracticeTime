

$(document).ready(function () {
    var timeZoneOffset = new Date().getTimezoneOffset();
    $("#slider").slider({
        slide: function(event, ui) {
            var value = $("#slider").slider("option", "value");
            $("#Time").val(value);
            $("#slider-value").text(value);
        }
    });
    try {
        var data = $("#Title").data("titles");
        var titlesData = data.split(',');
        $("#Title").autocomplete({ source: titlesData });
    } catch (err)
    {
    }

    var datePickerFormat = 'mm/dd/yy';
    $(".datepicker").datepicker({
        dateFormat: datePickerFormat,
        maxDate: '+0D'
    });

    var currentDate = $.datepicker.formatDate(datePickerFormat, new Date());

    $(".datepicker").val(currentDate);
    $("#TimeZoneOffset").val(timeZoneOffset);
});
