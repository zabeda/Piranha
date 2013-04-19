/**
* jQuery methods for handling attachments.
*
* 2012-04-10   @tidyui
*/

function bindAttachmentEvents() {
    //
    // Event hander for switching to the attachment view.
    //
    $("#btn_attachments").unbind();
    $("#btn_attachments").click(function () {
        // Hide content editors
        hideEditors();

        // Show the attachment view
        $(this).addClass("active");
        $(".edit td").removeClass("active");
        $("#attachments").show();
        return false;
    });

    //
    // Deletes a row from the attachment list.
    //
    $("#attachments .delete").unbind();
    $("#attachments .delete").click(function () {
        var row = $(this).parent().parent();
        $('#attachments input[value="' + row.attr("data-id") + '"]').remove();
        row.remove();
    });
    $("#attachments .up").unbind();
    $("#attachments .up").click(function () {
        var row = $(this).parent().parent();
        if (row.parent().children().index(row) > 1)
            row.insertBefore(row.prev());
    });
    $("#attachments .down").unbind();
    $("#attachments .down").click(function () {
        var row = $(this).parent().parent();
        if (row.next())
            row.insertAfter(row.next());
    });

    //
    // Deletes a row from the attachment list.
    //
    $("#attachments .delete").unbind();
    $("#attachments .delete").click(function() {
        var row = $(this).parent().parent();
        $('#attachments input[value="' + row.attr("data-id") + '"]').remove();
        row.remove();
    });

    $("form").unbind();
    $("form").submit(function () {
        // Build Attachments
        $.each($("#attachments tr"), function (index, val) {
            if (index > 0) {
                addAttachmentData(index - 1, val);
            }
        });
    });
}

$(document).ready(function () {
    bindAttachmentEvents();
});