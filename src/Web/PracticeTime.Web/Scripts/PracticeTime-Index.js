

var time = new Date();
google.load('visualization', '1.0', { 'packages': ['corechart'] });

google.setOnLoadCallback(drawChart_chart_div_timedate);


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
        'width': 400,
        'height': 300
    };
    var chart = new google.visualization.LineChart(document.getElementById('chart_div_timedate'));
    chart.draw(data, options);

    drawChart_chart_div_timetitle();
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
        'width': 400,
        'height': 300,
        'orientation' : 'horizontal'
    };
    var chart = new google.visualization.BarChart(document.getElementById('chart_div_timetitle'));
    chart.draw(data, options);
}
