let host = window.location.hostname;
if (host.split("localhost").length == 2) {
    host = "/";
} else {
    host = "/2036516/";
}

let formGroups = document.querySelectorAll(".form-group");

for (let formGroup of formGroups) {

    let errorZone = formGroup.querySelector(".text-danger");
    let input = formGroup.querySelector(".form-control");

    if (errorZone != undefined && errorZone.innerHTML != "") {

        if (input != undefined) {

            input.classList.add("input-error");
        }
    } else {

        if (input != undefined) {

            input.classList.remove("input-error");
        }
    }
}
