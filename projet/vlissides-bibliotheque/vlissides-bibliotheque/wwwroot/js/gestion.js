
function getFormulaireModifierEtudiant(id) {

    fetch(host + "TableauDeBord/ModifierEtudiant/" + String(id), {
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

function modifierEtudiant(id) {
    let parent = document.querySelector("#etudiant-" + String(id))
        .querySelector(".modal-body");

    let formulaire = parent.querySelector("form");

    let formData = new FormData(formulaire);

    let data = {};
    formData.forEach(function (value, key) {
        data[key] = value;
    });

    fetch(host + "TableauDeBord/ModifierEtudiant/", {
        method: 'POST',
        body: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        }
    }).then(function (res) {
        if (!res.ok) {
            alert("Aucune modification n'a pu être effectuée.")
        }

        res.text().then(function (res) {
            parent.innerHTML = res;
            setInputsFormat();
        });
    });
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