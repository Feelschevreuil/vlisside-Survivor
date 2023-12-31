﻿function afficherModificationLivre(id, data) {
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
            } else if (key == "DatePublication") {
                baliseInfo.innerHTML = data.DateFormater;
            }else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }

    }
}

function afficherCreationLivre(id, data) {
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
            } else if (key == "DatePublication") {
                baliseInfo.innerHTML = data.DateFormater;
            } else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }
        if (key == "MaisonsDeditions") {
            baliseInfo = ligneCourante.children[4];
            baliseInfo.innerHTML = document.querySelector("#MaisonDeditionId")[data.MaisonDeditionId].innerHTML
        }

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

function getAuteursCheckBox() {
    var divListAuteurs = document.querySelector("#listAuteurs");
    var auteursCocher = divListAuteurs.querySelectorAll("input");
    var listAuteursCocher = new Array();

    auteursCocher.forEach((auteurs) => {
        if (auteurs.checked) {
            listAuteursCocher.push(auteurs.id);
        }
    });
    return listAuteursCocher;
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

function creerBtnModifSuppriLivre(nouvelleLigne, id) {
    nouvelleLigne.classList.add("modif-suppr");
    trBtn = document.createElement("td");
    trBtn.classList.add("options-ligne", "position-absolute", "text-center", "vw-100", "start-0", "bg-transparent", "border-0")
    nouvelleLigne.appendChild(trBtn);
    editImg = document.createElement("img")
    editImg.classList.add("btn-img-hover");
    editImg.setAttribute("data-bs-toggle", "modal");
    editImg.setAttribute("data-bs-target", "#modal-modifier");
    editImg.setAttribute("onclick", 'getFormulaireModifierLivre("'+ id +'")');
    editImg.setAttribute("src", "/img/pencil.svg");
    trBtn.appendChild(editImg);

    deleteImg = document.createElement("img");
    deleteImg.classList.add("btn-img-hover");
    deleteImg.setAttribute("onclick", 'supprimerLivre("'+ id +'")');
    deleteImg.setAttribute("src", "/img/delete.svg");
    trBtn.appendChild(deleteImg);
    return nouvelleLigne;
}

//--------------------------------------------
//              Livres
//--------------------------------------------
function getPartialViewLivre() {
    fetch(host + "TableauDeBord/Livres/", {
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

function getFormulaireModifierLivre(id) {

    fetch(host + "TableauDeBord/ModifierLivre/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Le livre est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#modal-modifier").querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierLivre() {
    let parent = document.querySelector("#modal-modifier").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    data = livreGestionErreur(data);
    data.Cours = getCoursCheckBox();
    data.Auteurs = getAuteursCheckBox();

    fetch(host + "TableauDeBord/ModifierLivre/", {
        method: 'POST',
        body: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        }
    }).then(function (res) {
        if (!res.ok) {
            res.text();
            var pageCourante = document.querySelector("#PageCourante");
            pageCourante.innerHTML = data;
            alert("Aucune modification n'a pu être effectuée.")
        }
        // valider si le contenu reçu est du json ou du text
        const contentType = res.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1) {

            res.json().then(function (res) {
                if (res != "") {
                    afficherModificationLivre(res.idDuLivre, resetMajusculeJsonKey(res));
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

function getFormulaireCreerLivre() {

    fetch(host + "TableauDeBord/CreerLivre/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerLivre() {
    let parent = document.querySelector("#creer").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    data = livreGestionErreur(data);
    data.Cours = getCoursCheckBox();
    data.Auteurs = getAuteursCheckBox();
    data.id = 0;

    fetch(host + "TableauDeBord/CreerLivre/", {
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
                    nouvelleLigne.classList.add('modif-suppr');
                    let id = res.id;
                    nouvelleLigne.id = "tr-" + id;
                    for (let i = 0; i < thead.children[0].childElementCount; i++) {
                        nouvelleLigne.appendChild(document.createElement("td"));
                    }
                    nouvelleLigne = creerBtnModifSuppriLivre(nouvelleLigne, id);
                    tbody.insertBefore(nouvelleLigne, tbody.children[0]);
                    afficherCreationLivre(id, resetMajusculeJsonKey(res));
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

function supprimerLivre(id) {
    var confirmation = confirm("Êtes-vous sur de vouloir supprimer cet livre?");
    if (confirmation) {
        fetch(host + "TableauDeBord/SupprimerLivre/", {
            method: 'POST',
            body: JSON.stringify(id),
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-Type": "application/json",
            }
        }).then(function (res) {
            if (res.ok) {
                alert("Livre supprimé avec succès!");
                let livre = document.querySelector(`#tr-${id}`);
                let parent = livre.parentElement;
                parent.removeChild(livre);
            } else {
                alert(`Impossible de supprimer le livre selon le code d'identification ${id}`);
            }
        });
    }
}