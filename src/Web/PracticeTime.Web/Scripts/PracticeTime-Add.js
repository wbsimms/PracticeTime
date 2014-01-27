

$(document).ready(function () {
    $("#slider").slider({
        change: function(event, ui) {
            var value = $("#slider").slider("option", "value");
            $("#Time").val(value);
            $("#slider-value").text(value);
        }
    });
    var titlesData;
    try {
        var data = $("#Title").data("titles");
        var titlesData = data.split(',');
        $("#Title").autocomplete({ source: titlesData });
    } catch (err)
    {
    }
});
