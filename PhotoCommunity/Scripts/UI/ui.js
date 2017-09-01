$('#file-button').change(function () {
    var pathImage = document.getElementById("file-button").value;
    document.getElementById("uploadFile").value = pathImage.split("\\")[pathImage.split("\\").length - 1];
});

function OnSuccess(data, upgrateId) {
    if (upgrateId === "results") {
        ClearTextBox();
    }
    if (data.split("error").length > 1) {
        var errMsg = data.split("error")[1];
        $("#err-block h3").html(errMsg);
        $("#err-block").show().fadeOut(5000);
    }
    else {        
        $("#"+upgrateId).html(data);
    }
}

function ClearTextBox() {
    $('[name="comment"]').val("");
}