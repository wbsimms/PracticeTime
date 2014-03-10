
$(document).ready(function() {
    
    $("#musicians-data-table").dataTable(
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
});