/**
* jQuery methods for the manager page views.
*
* 2012-03-13   @tidyui
*/

function bindAttachmentEvents() {
    //
    // Deletes a row from the attachment list.
    //
    $("#attachments .delete").click(function () {
        var row = $(this).parent().parent();
        $('#attachments input[value="' + row.attr("data-id") + '"]').remove();
        row.remove();
    });
    $("#attachments .up").click(function () {
        var row = $(this).parent().parent();
        if (row.parent().children().index(row) > 1)
            row.insertBefore(row.prev());
    });
    $("#attachments .down").click(function () {
        var row = $(this).parent().parent();
        if (row.next())
            row.insertAfter(row.next());
    });
}

$(document).ready(function () {
    bindAttachmentEvents();
});