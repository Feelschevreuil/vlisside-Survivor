
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
            if (res == "") {
                afficherModification(id, data);
                document.querySelector(`#fermer-modal-${id}`).click();
            }
        });
    });
}

// afficher les valeurs modifiées et non modifiées
function afficherModification(id, data) {
    let table = document.querySelectorAll("table")[0];
    let thead = table.children[0];
    let champs = Array.from(thead.children[0].children);
    let etudiantCourant = document.querySelector(`#tr-${id}`);

    for (let key in data) {
        let champ = champs.find(th => th.innerHTML == key);
        let index = champs.indexOf(champ);
        let baliseInfo = etudiantCourant.children[index];
        if (baliseInfo != undefined) {
            baliseInfo.innerHTML = data[`${key}`];
        }
    }
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