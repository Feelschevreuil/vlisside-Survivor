
function getFormulaireModifierEtudiant(id) {

    fetch("/TableauDeBord/ModifierEtudiant/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("L'étudiant est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#etudiant-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
            setInputsFormat();
        });
    });
}

function modifierEtudiant() {
    //fetch("/TableauDeBord/ModifierEtudiant/" + String(id), {
    //    method: 'GET',
    //    body: data,
    //    contentType: "application/json; charset=utf-8",
    //    headers: {
    //        "Content-Type": "application/json",
    //    }
    //}).then(function (res) {
    //    if (!res.ok) {
    //        alert("L'étudiant est introuvable.")
    //    }

    //    res.text().then(function (res) {
    //        document.querySelector("#etudiant-" + String(id))
    //            .querySelector(".modal-body").innerHTML = res;
    //    });
    //});
}

function setInputsFormat() {
    // appel initial pour setter les inputs en cas où ils auraient déjà une valeur
    for (let phoneNumberInput of document.querySelectorAll("#phoneNumber")) {
        setPhoneNumber(phoneNumberInput.parentElement.children[1]);
    }
    for (let postalCodeInput of document.querySelectorAll("#postalCode")) {
        setPostalCode(postalCodeInput.parentElement.children[1]);
    }
}