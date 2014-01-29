

var time = new Date();
google.load('visualization', '1.0', { 'packages': ['corechart'] });

google.setOnLoadCallback(drawChart);

//setInterval(drawChart, 1000);

function drawChart() {

    var dataTable;
    $.ajax({
        url: "/Sessions/GetSessionsForUser",
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
        'title': 'Practice Time',
        'width': 400,
        'height': 300
    };
    var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}
