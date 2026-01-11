$(document)
    .on("focusin", "input[title], textarea[title], select[title]", function () {
 
        if ($(this).data("ui-tooltip")) {
            $(this).tooltip("close");
        }
    });