$.validator.addMethod("fileextensions",
    function (value, element, param) {
        var fileType = $(element)[0].files[0].type;
        var fileTypes = ["application/pdf"]
        if (fileType) {
            var validExtension = $.inArray(fileType, fileTypes) !== -1;
            return validExtension;
        } else {
            return false;
        }        
    });

$.validator.addMethod("maxfilesize",
    function (value, element, param) {

        var fileSize = $(element)[0].files[0].size;
        var maxSize = 10048576;

        var validSize = fileSize < maxSize;
        return validSize;
    });

$.validator.unobtrusive.adapters.add('fileextensions', [], function (options) {
    var params = {
        fileextensions: $(options.element).data("val-fileextensions").split(',')
    };

    options.rules['fileextensions'] = params;
    options.messages['fileextensions'] = $(options.element).data("val-fileextensions");

});

$.validator.unobtrusive.adapters.add('maxfilesize', [], function (options) {

    options.rules['maxfilesize'] = [];
    options.messages['maxfilesize'] = $(options.element).data("val-maxfilesize");

});