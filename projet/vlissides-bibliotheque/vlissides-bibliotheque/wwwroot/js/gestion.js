//--------------------------------------------
//              Réutilisable
//--------------------------------------------
function getFormData(formulaire) {
    let formData = new FormData(formulaire);
    let data = {};

    formData.forEach(function (value, key) {
        data[key] = value;
    });

    return data;
}

function resetMajusculeJsonKey(json) {
    let data = {};

    for (let key in json) {
        data[setPremiereLettreEnMajuscule(key)] = json[key];
    }

    return data;
}

function setPremiereLettreEnMajuscule(string) {
    return string[0].toUpperCase() + string.slice(1);
}

function afficherModification(id, data) {
    // afficher les valeurs modifiées et non modifiées

    let table = document.querySelectorAll("table")[0];
    let thead = table.children[0];
    let champs = Array.from(thead.children[0].children);
    let ligneCourante = document.querySelector(`#tr-${id}`);

    for (let key in data) {
        let champ = champs.find(th => th.id == key);
        let index = champs.indexOf(champ);
        let baliseInfo = ligneCourante.children[index];
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

//--------------------------------------------
//              Étudiants
//--------------------------------------------

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
    let parent = document.querySelector("#etudiant-" + String(id)).querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

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
        // valider si le contenu reçu est du json ou du text
        const contentType = res.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1) {

            res.json().then(function (res) {
                if (res != "") {
                    afficherModification(id, resetMajusculeJsonKey(res));
                    document.querySelector(`#fermer-modal-${id}`).click();
                }
            });
        } else {

            res.text().then(function (res) {
                parent.innerHTML = res;
                setInputsFormat();
            });
        }
    });
}

function getFormulaireCreerEtudiant() {

    fetch(host + "TableauDeBord/CreerEtudiant/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer").querySelector(".modal-body").innerHTML = res;
                setInputsFormat();
            });
        }
    });
}

function creerEtudiant() {
    let parent = document.querySelector("#creer").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/CreerEtudiant/", {
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
        // valider si le contenu reçu est du json ou du text
        const contentType = res.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1) {

            res.json().then(function (res) {
                if (res != "") {
                    let table = document.querySelectorAll("table")[0];
                    let thead = table.children[0];
                    let tbody = table.children[1];
                    let nouvelleLigne = document.createElement("tr");
                    let id = res.etudiantId;
                    nouvelleLigne.id = "tr-" + id;
                    for (let i = 0; i < thead.children[0].childElementCount; i++) {
                        nouvelleLigne.appendChild(document.createElement("td"));
                    }
                    tbody.insertBefore(nouvelleLigne, tbody.children[0]);
                    afficherModification(id, resetMajusculeJsonKey(res));
                    document.querySelector("#fermer-modal-creer").click();
                }
            });
        } else {

            res.text().then(function (res) {
                parent.innerHTML = res;
                setInputsFormat();
            });
        }
    });
}

function supprimerEtudiant(id) {
    fetch(host + "TableauDeBord/SupprimerEtudiant/", {
        method: 'POST',
        body: JSON.stringify(id),
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        }
    }).then(function (res) {
        if (res.ok) {
            alert("Étudiant supprimé avec succès!");
            let etudiant = document.querySelector(`#tr-${id}`);
            let parent = etudiant.parentElement;
            parent.removeChild(etudiant);
        } else {
            alert(`Impossible de supprimer l'étudiant selon le code d'identification ${id}`);
        }
    });
}

//--------------------------------------------
//              Livres
//--------------------------------------------

//--------------------------------------------
//              Cours
//--------------------------------------------

//--------------------------------------------
//              Programmes d'étude
//--------------------------------------------

//--------------------------------------------
//              Commandes
//--------------------------------------------

//--------------------------------------------
//              Promotions
//--------------------------------------------