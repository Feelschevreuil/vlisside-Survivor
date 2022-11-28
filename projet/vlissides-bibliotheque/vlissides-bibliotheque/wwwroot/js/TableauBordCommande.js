﻿function getFormData(formulaire) {
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
                baliseInfo.children[0].src = data.Photo;
            } else if (key == "ProgrammeEtude") {
                baliseInfo = ligneCourante.children[3];
                index = document.querySelector('#ProgrammesEtudeId').selectedIndex
                baliseInfo.innerHTML = document.querySelector('#ProgrammesEtudeId')[index].innerHTML
            } else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }

    }
}

function afficherCreation(id, data) {
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

function getCoursCheckBox() {
    var divListCours = document.querySelector("#listDeCours");
    var coursCocher = divListCours.querySelectorAll("input");
    var listCoursCocher = new Array();

    coursCocher.forEach((cours) => {
        if (cours.checked) {
            listCoursCocher.push(cours.id);
        }
    });
    return listCoursCocher;
}

function livreGestionErreur(data) {
    if (data.AuteurId == "") { data.AuteurId = 0 }
    if (data.MaisonDeditionId == "") { data.MaisonDeditionId = 0 }
    data.PossedeNeuf = document.querySelector("#PossedeNeuf").checked;
    data.PossedeNumerique = document.querySelector("#PossedeNumerique").checked;
    data.PossedeUsagee = document.querySelector("#PossedeUsagee").checked;
    data.PrixNeuf = possedeDesLettres(data.PrixNeuf);
    data.PrixNumerique = possedeDesLettres(data.PrixNumerique);
    data.PrixUsage = possedeDesLettres(data.PrixUsage);
    data.QuantiteUsagee = possedeDesLettres(data.QuantiteUsagee);
    return data
}

function EtudiantGestionErreur(data) {
    if (data.App == "") { data.App = 0 }
    return data;
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

function creerBtnModifSuppri(nouvelleLigne, id) {
    nouvelleLigne.classList.add("modif-suppr");
    trBtn = document.createElement("td");
    trBtn.classList.add("options-ligne", "position-absolute", "text-center", "vw-100", "start-0", "bg-transparent", "border-0")
    nouvelleLigne.appendChild(trBtn);
    editImg = document.createElement("img")
    editImg.classList.add("btn-img-hover");
    editImg.setAttribute("data-bs-toggle", "modal");
    editImg.setAttribute("data-bs-target", "modal-modifier");
    editImg.setAttribute("onclick", "getFormulaireModifierLivre(" + id + ")");
    editImg.setAttribute("src", "/img/pencil.svg");
    trBtn.appendChild(editImg);

    deleteImg = document.createElement("img");
    deleteImg.classList.add("btn-img-hover");
    deleteImg.setAttribute("onclick", "supprimerLivre(" + id + ")");
    deleteImg.setAttribute("src", "/img/delete.svg");
    trBtn.appendChild(deleteImg);
    return nouvelleLigne;
}

function getLivreId() {
    let parent = document.querySelector("#modal-modifier").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let livreId = formulaire.IdDuLivre;
    return livreId;
}


//--------------------------------------------
//              Commandes
//--------------------------------------------
function getPartialViewCommandes() {
    fetch(host + "TableauDeBord/Commandes/", {
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

function getFormulaireModifierCommande(id) {

    fetch(host + "TableauDeBord/ModifierCommandes/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Le promotions est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#modal-modifier")
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierCommandes() {
    let parent = document.querySelector("#modal-modifier").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);
    data.Id = 0
    fetch(host + "TableauDeBord/ModifierCommandes/", {
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
                    afficherModification(res.id, resetMajusculeJsonKey(res));
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

function getFormulaireCreerCommande() {

    fetch(host + "TableauDeBord/CreerCommandes/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerCommande() {
    let parent = document.querySelector("#creer").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/CreerCommandes/", {
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
                    nouvelleLigne = creerBtnModifSuppri(nouvelleLigne, id);
                    tbody.insertBefore(nouvelleLigne, tbody.children[0]);
                    afficherCreation(id, resetMajusculeJsonKey(res));
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

function supprimerCommande(id) {
    var confirmation = confirm("Êtes-vous sur de vouloir supprimer cet évênement?");
    if (confirmation) {
        fetch(host + "TableauDeBord/SupprimerCommande/", {
            method: 'POST',
            body: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-Type": "application/json",
            }
        }).then(function (res) {
            if (res.ok) {
                alert("Promotions supprimé avec succès!");
                let promotions = document.querySelector(`#tr-${id}`);
                let parent = promotions.parentElement;
                parent.removeChild(promotions);
            } else {
                alert(`Impossible de supprimer le promotions selon le code d'identification ${id}`);
            }
        });
    }
}