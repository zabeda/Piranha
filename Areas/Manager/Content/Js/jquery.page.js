/**
 * jQuery methods for the manager page views.
 *
 * 2012-03-13   @tidyui
 */

$(document).ready(function () {
    // Show the first page region.
    $("#pageregions .input:first").show();
    var firstid = $("#pageregions .input:first").attr("id");
    if (firstid) {
        $("#" + firstid).addClass("active");
        $(".edit td").removeClass("active");
        $(".edit #" + firstid).addClass("active");
    }

    // 
    // Event handler for switching page regions.
    //
    $(".pageregion").click(function () {
        var id = $(this).attr("id").substring(4);

        hideEditors();
        $("#pageregions #" + id).show();
        $(this).addClass("active");
        $(".edit td").removeClass("active");
        $(".edit #" + id).addClass("active");

        return false;
    });

    //
    // Event handler for switching page regions by clicking 
    // in the template preview.
    //
    $(".edit td").click(function () {
        if (!$(this).hasClass("locked")) {
            var id = $(this).attr("id");

            hideEditors();
            $("#pageregions #" + id).show();
            $(".edit td").removeClass("active");
            $(this).addClass("active");
            $("#btn_" + id).addClass("active");
        }
    });
});
