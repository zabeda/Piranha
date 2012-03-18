/**
* jQuery methods for the manager page views.
*
* 2012-03-13   @tidyui
*/

$(document).ready(function() {
    //
    // Deletes a row from the attachment list.
    //
    $("#attachments .delete").click(function() {
        var row = $(this).parent().parent();
        $('#attachments input[value="' + row.attr("data-id") + '"]').remove();
        row.remove();
    });
});