
$("#slider").slider({
    change: function(event, ui) {
        var value = $("#slider").slider("option", "value");
        $("#Time").val(value);
    }
});

$("#Title").autocomplete({ source: titles });
