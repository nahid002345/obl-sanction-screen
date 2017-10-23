function LogOutNow() {

    PageMethods.LogOutN(onSuccessLogout, onFailure);
}


function ShowAlertBox() {
    $('.alert-danger').css("display", "inline-block");
}

function ShowSuccessBox() {
    $('.alert-success').css("display", "inline-block");
}

