/**
* jQuery methods for handling attachments.
*
* 2012-04-10   @tidyui
*/

if (!piranha)
    var piranha = {};

piranha.folderId = null;
piranha.media = function (buttonId, floatboxId, callback) {
    var self = this;

    this.btnId = buttonId;
    this.boxId = floatboxId;
    this.cb = callback;
    this.tinymce = false;
    this.filter = 'none';

    this.buttonClick = function () {
        $('#' + self.boxId + ' .box > div').remove();
        floatBox.show(self.boxId);
        $.ajax({
            url: siteroot + 'manager/content/popup' + (piranha.folderId ? '/' + piranha.folderId : '') + '?tinymce=' + self.tinymce + '&filter=' + self.filter,
            success: function (data) {
                $('#' + self.boxId + ' .box').html('');
                $('#' + self.boxId + ' .box').append(data);
                floatBox.position($('#' + self.boxId + ' .box'));
            }
        });
    };
    this.mediaClick = function () {
        var that = this;

        if (!$(this).hasClass("folder")) {
            $('#' + self.boxId + ' .inner .loader').fadeIn('fast', function () {
                $.ajax({
                    url: siteroot + "manager/content/get/" + $(that).attr("data-id") + '?tinymce=' + self.tinymce,
                    dataType: "json",
                    success: function (data) {
                        if (self.cb)
                            self.cb(data);
                        floatBox.close(self.boxId);
                    }
                });
            });
        }
    };
    this.folderClick = function () {
        var that = this;

        piranha.folderId = $(this).attr("data-id");

        $('#' + self.boxId + ' .inner .loader').fadeIn('fast', function () {
            $.ajax({
                url: siteroot + "manager/content/popup/" + $(that).attr("data-id") + '?tinymce=' + self.tinymce + '&filter=' + self.filter,
                success: function (data) {
                    $('#' + self.boxId + ' .box').html('');
                    $('#' + self.boxId + ' .box').append(data);
                    floatBox.position($('#' + self.boxId + ' .box'));
                }
            });
        });
    };
    this.boxClose = function () {
        floatBox.close(self.boxId);
    };

    $('#' + this.boxId + ' .gallery-item img').live('click', this.mediaClick);
    $('#' + this.boxId + ' .gallery-item img.folder').live('click', this.folderClick);
    $('#' + this.btnId).live('click', this.buttonClick);
    $('#' + this.boxId + ' .title .close-btn').live('click', this.boxClose);
};
