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

function afficherModificationCours(id, data) {
    // affiche les valeurs modifiées et non modifiées
    let table = document.querySelectorAll("table")[0];
    let thead = table.children[0];
    let champs = Array.from(thead.children[0].children);
    let ligneCourante = document.querySelector(`#tr-${id}`);

    for (let key in data) {
        let champ = champs.find(th => th.id == key);
        let index = champs.indexOf(champ);
        let baliseInfo = ligneCourante.children[index];

        if (baliseInfo != undefined) {
            if (key == "Nom") {
                baliseInfo = ligneCourante.children[3];
                index = document.querySelector('#ProgrammesEtudeId').selectedIndex
                baliseInfo.innerHTML = document.querySelector('#ProgrammesEtudeId')[index].innerHTML
            } else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }

    }
}

function afficherCreationCours(id, data) {
    // affiche les valeurs modifiées et non modifiées
    let table = document.querySelectorAll("table")[0];
    let thead = table.children[0];
    let champs = Array.from(thead.children[0].children);
    let ligneCourante = document.querySelector(`#tr-${id}`);

    for (let key in data) {
        let champ = champs.find(th => th.id == key);
        let index = champs.indexOf(champ);
        let baliseInfo = ligneCourante.children[index];

        if (baliseInfo != undefined) {
            if (key == "Photo") {
                var img = document.createElement('img');
                img.src = data.Photo
                img.classList.add('tableauDeBord-image');
                baliseInfo.appendChild(img);
                baliseInfo.classList.add('text-center');
            } else if (key == "ProgrammeEtude") {
                baliseInfo = ligneCourante.children[3];
                index = document.querySelector('#ProgrammesEtudeId').selectedIndex
                baliseInfo.innerHTML = document.querySelector('#ProgrammesEtudeId')[index].innerHTML
            } else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }
        if (key == "MaisonsDeditions") {
            baliseInfo = ligneCourante.children[5];
            baliseInfo.innerHTML = document.querySelector("#MaisonDeditionId")[data.MaisonDeditionId].innerHTML
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

function CoursGestionErreur(data) {
    if (data.programmeEtudesId == "") { data.programmeEtudesId = 0 }
    if (data.ProgrammesEtudeId == "") { data.ProgrammesEtudeId = 0 }

    return data
}

function possedeDesLettres(nombre) {
    if (isNaN(parseFloat(nombre)) || nombre == "") {
        return nombre = 0;
    }
    else {
        return parseFloat(nombre.replace(",", "."));
    }
}

function creerBtnModifSuppriCours(nouvelleLigne, id) {
    nouvelleLigne.classList.add("modif-suppr");
    trBtn = document.createElement("td");
    trBtn.classList.add("options-ligne", "position-absolute", "text-center", "vw-100", "start-0", "bg-transparent", "border-0")
    nouvelleLigne.appendChild(trBtn);
    editImg = document.createElement("img")
    editImg.classList.add("btn-img-hover");
    editImg.setAttribute("data-bs-toggle", "modal");
    editImg.setAttribute("data-bs-target", "#modal-modifier");
    editImg.setAttribute("onclick", "getFormulaireModifierCours(" + id + ")");
    editImg.setAttribute("src", "/img/pencil.svg");
    trBtn.appendChild(editImg);

    deleteImg = document.createElement("img");
    deleteImg.classList.add("btn-img-hover");
    deleteImg.setAttribute("onclick", "supprimerCours(" + id + ")");
    deleteImg.setAttribute("src", "/img/delete.svg");
    trBtn.appendChild(deleteImg);
    return nouvelleLigne;
}

//--------------------------------------------
//              Cours
//--------------------------------------------
function getPartialViewCours() {
    fetch(host + "TableauDeBord/Cours/", {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Une erreur c'est produite")
        }
        res.text().then(function (res) {
            document.querySelector("#partials").innerHTML = res;
        });
    });
}

function getFormulaireModifierCours(id) {

    fetch(host + "TableauDeBord/ModifierCours/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Le cours est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#modal-modifier")
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierCours() {
    let parent = document.querySelector("#modal-modifier").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/ModifierCours/", {
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
                    afficherModificationCours(res.id, resetMajusculeJsonKey(res));
                    document.querySelector("#fermer-modal-modifier").click();
                }
            });
        } else {

            res.text().then(function (res) {
                parent.innerHTML = res;
            });
        }
    });
}

function getFormulaireCreerCours() {

    fetch(host + "TableauDeBord/CreerCours/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerCours() {
    let parent = document.querySelector("#creer").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);
    data = CoursGestionErreur(data);
    data.Id = 0;

    fetch(host + "TableauDeBord/CreerCours/", {
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
                    let id = res.id;
                    nouvelleLigne.id = "tr-" + id;
                    for (let i = 0; i < thead.children[0].childElementCount; i++) {
                        nouvelleLigne.appendChild(document.createElement("td"));
                    }
                    nouvelleLigne = creerBtnModifSuppriCours(nouvelleLigne, id);
                    tbody.insertBefore(nouvelleLigne, tbody.children[0]);
                    afficherCreationCours(id, resetMajusculeJsonKey(res));
                    document.querySelector("#fermer-modal-creer").click();
                }
            });
        } else {

            res.text().then(function (res) {
                parent.innerHTML = res;
            });
        }
    });
}

function supprimerCours(id) {
    var confirmation = confirm("Êtes-vous sur de vouloir supprimer ce cours?");
    if (confirmation) {
        fetch(host + "TableauDeBord/SupprimerCours/", {
            method: 'POST',
            body: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-Type": "application/json",
            }
        }).then(function (res) {
            if (res.ok) {
                alert("Cours supprimé avec succès!");
                let cours = document.querySelector(`#tr-${id}`);
                let parent = cours.parentElement;
                parent.removeChild(cours);
            } else {
                alert(`Impossible de supprimer le cours selon le code d'identification ${id}`);
            }
        });
    }
}