let formGroups = document.querySelectorAll(".form-group");

for (let formGroup of formGroups) {

    let errorZone = formGroup.querySelector(".text-danger");
    let input = formGroup.querySelector(".form-control");

    if (errorZone != undefined && errorZone.innerHTML != "") {

        if (input != undefined) {

            input.classList.remove("input-ok");
            input.classList.add("input-error");
        }
    } else {

        if (input != undefined) {

            input.classList.remove("input-error");
            input.classList.add("input-ok");
        }
    }
}
