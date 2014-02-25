


google.load('visualization', '1.0', { 'packages': ['corechart'] });
google.setOnLoadCallback(draw_charts);

function draw_charts() {
    drawChart_chart_div_timedate();
    drawChart_chart_div_timetitle();
    drawChart_chart_div_timeinstruments();
    $("#student-data-table").dataTable(
    {
        "sDom": "<'row'<'span6'l><'span6'f>r>t<'row'<'span6'i><'span6'p>>",
        "bPaginate": false,
        "bLengthChange": true,
        "bFilter": false,
        "bSort": true,
        "bInfo": false,
        "bAutoWidth": true
    });
    $.extend($.fn.dataTableExt.oStdClasses, {
        "sWrapper": "dataTables_wrapper form-inline"
    });

}

function drawChart_chart_div_timedate() {

    var dataTable;
    $.ajax({
        url: "/Sessions/GetSessionsForUserGraph",
        dataType: 'json',
        async: false,
        type: "POST"
    }).success(function(data) {
        var jdata = $.parseJSON(data);
        for (var i in jdata.rows) {
            var dateString = jdata.rows[i].c[0].v;
            jdata.rows[i].c[0].v = new Date(dateString);
        }
        dataTable = jdata;
    });
    var data = new google.visualization.DataTable(dataTable);

    var options = {
        'title': 'Practice Time by Date',
        'width': 300,
        'height': 200,
        'legend': { position: "none" }

    };
    var chart = new google.visualization.LineChart(document.getElementById('chart_div_timedate'));
    chart.draw(data, options);
}

function drawChart_chart_div_timetitle() {

    var dataTable;
    $.ajax({
        url: "/Sessions/GetSessionsForUserGraphTitle",
        dataType: 'json',
        async: false,
        type: "POST"
    }).success(function (data) {
        dataTable = data;
    });
    var data = new google.visualization.DataTable(dataTable);

    var options = {
        'title': 'Practice Time per Passage',
        'width': 300,
        'height': 200,
        'orientation': 'horizontal',
        'legend': { position: "none" }

    };
    var chart = new google.visualization.BarChart(document.getElementById('chart_div_timetitle'));
    chart.draw(data, options);
}

function drawChart_chart_div_timeinstruments() {

    var dataTable;
    $.ajax({
        url: "/Sessions/GetSessionsForUserGraphInstruments",
        dataType: 'json',
        async: false,
        type: "POST"
    }).success(function (data) {
        dataTable = data;
    });
    var data = new google.visualization.DataTable(dataTable);

    var options = {
        'title': 'Practice Time per Instrument',
        'width': 300,
        'height': 200,
        'orientation': 'horizontal',
        'legend': { position: "none" }
    };
    var chart = new google.visualization.BarChart(document.getElementById('chart_div_timeinstruments'));
    chart.draw(data, options);
}

