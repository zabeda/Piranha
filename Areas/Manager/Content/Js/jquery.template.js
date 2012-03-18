/**
 * jQuery methods for the manager template views.
 *
 * 2012-03-13   @tidyui
 */

$(document).ready(function () {
    bindEvents();

    //
    // Process the form data some before sending it back to the server
    //
    $("form").submit(function () {
        // Build page regions
        $.each($("#pageregions").children(), function (index, val) {
            $("#region_data").append(
                '<input id="Template_PageRegions_' + index +
                '_" name="Template.PageRegions[' + index +
                ']" type="hidden" value="' + $(val).children("span:first").text() + '" />');
        });
        // Build Properties
        $.each($("#properties").children(), function (index, val) {
            $("#region_data").append(
                '<input id="Template_Properties_' + index +
                '_" name="Template.Properties[' + index +
                ']" type="hidden" value="' + $(val).children("span:first").text() + '" />');
        });
    });
});

//
// Binds the events associated with the region lists. This method is executed
// every time an item is added or removed as this updates the DOM.
//
function bindEvents() {
    $("#pr_add").unbind();
    $("#pr_add").click(function () {
        var name = $("#pr_name").val();

        if (name != null && name != "") {
            $("#pageregions").append(
                '<li><span>' + name + '</span><button class="btn delete right remove-region"></button></li>');
            $("#pr_name").val("");
            bindEvents();
        } else alert("Du måste ange ett namn för regionen.");
        return false;
    });

    $("#po_add").unbind();
    $("#po_add").click(function () {
        var name = $("#po_name").val();

        if (name != null && name != "") {
            $("#properties").append(
                '<li><span>' + name + '</span><button class="btn delete right remove-region"></button></li>');
            $("#po_name").val("");
            bindEvents();
        } else alert("Du måste ange ett namn för egenskapen.");
        return false;
    });

    $(".remove-region").unbind();
    $(".remove-region").click(function () {
        $(this).parent().remove();
    });
}