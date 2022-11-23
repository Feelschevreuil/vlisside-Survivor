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

        if (baliseInfo != undefined)
        {
            if (key == "Photo") {
                baliseInfo.children[0].src = data.Photo;
            } else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }
      
    }
}

function afficherCreation(id, data) {
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
            if (key == "Photo") {
                var img = document.createElement('img');
                img.src = data.Photo
                img.classList.add('tableauDeBord-image');
                baliseInfo.appendChild(img);
                baliseInfo.classList.add('text-center');
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

function getCoursCheckBox()
{
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

function possedeDesLettres(nombre) {
    if (isNaN(parseFloat(nombre)) || nombre == "") {
        return nombre = 0;
    }
    else {
        return parseFloat(nombre.replace(",", "."));
    }
}

//--------------------------------------------
//              Étudiants
//--------------------------------------------
function getPartialViewEtudiant() {
    fetch(host + "TableauDeBord/Etudiants/", {
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
            document.querySelector("#livre-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierLivre(id) {
    let parent = document.querySelector("#livre-" + String(id)).querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    data = livreGestionErreur(data);
    data.Cours = getCoursCheckBox();

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
                    afficherModification(id, resetMajusculeJsonKey(res));
                    document.querySelector(`#fermer-modal-${id}`).click();
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
                    let id = res.livreId;
                    nouvelleLigne.id = "tr-" + id;
                    for (let i = 0; i < thead.children[0].childElementCount; i++) {
                        nouvelleLigne.appendChild(document.createElement("td"));
                    }
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

function supprimerLivre(id) {
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
            document.querySelector("#cours-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierCours(id) {
    let parent = document.querySelector("#cours-" + String(id)).querySelector(".modal-body");
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
                    afficherModification(id, resetMajusculeJsonKey(res));
                    document.querySelector(`#fermer-modal-${id}`).click();
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
                    let id = res.coursId;
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
            });
        }
    });
}

function supprimerCours(id) {
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

//--------------------------------------------
//              Programmes d'étude
//--------------------------------------------
function getFormulaireModifierProgrammeEtudes(id) {

    fetch(host + "TableauDeBord/ModifierProgrammeEtudes/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Le programmeEtudes est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#programmeEtudes-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierProgrammeEtudes(id) {
    let parent = document.querySelector("#programmeEtudes-" + String(id)).querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/ModifierProgrammeEtudes/", {
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
            });
        }
    });
}

function getFormulaireCreerProgrammeEtudes() {

    fetch(host + "TableauDeBord/CreerProgrammeEtudes/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerProgrammeEtudes() {
    let parent = document.querySelector("#creer").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/CreerProgrammeEtudes/", {
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
                    let id = res.programmeEtudesId;
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
            });
        }
    });
}

function supprimerProgrammeEtudes(id) {
    fetch(host + "TableauDeBord/SupprimerProgrammeEtudes/", {
        method: 'POST',
        body: JSON.stringify(id),
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        }
    }).then(function (res) {
        if (res.ok) {
            alert("ProgrammeEtudes supprimé avec succès!");
            let programmeEtudes = document.querySelector(`#tr-${id}`);
            let parent = programmeEtudes.parentElement;
            parent.removeChild(programmeEtudes);
        } else {
            alert(`Impossible de supprimer le programmeEtudes selon le code d'identification ${id}`);
        }
    });
}
//--------------------------------------------
//              Commandes
//--------------------------------------------
function getFormulaireModifierCommandes(id) {

    fetch(host + "TableauDeBord/ModifierCommandes/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Le commandes est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#commandes-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierCommandes(id) {
    let parent = document.querySelector("#commandes-" + String(id)).querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

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
                    afficherModification(id, resetMajusculeJsonKey(res));
                    document.querySelector(`#fermer-modal-${id}`).click();
                }
            });
        } else {

            res.text().then(function (res) {
                parent.innerHTML = res;
            });
        }
    });
}

function getFormulaireCreerCommandes() {

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

function creerCommandes() {
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
                    let id = res.PrixEtatLivreId;
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
            });
        }
    });
}

function supprimerCommandes(id) {
    fetch(host + "TableauDeBord/SupprimerCommandes/", {
        method: 'POST',
        body: JSON.stringify(id),
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        }
    }).then(function (res) {
        if (res.ok) {
            alert("Commandes supprimé avec succès!");
            let commandes = document.querySelector(`#tr-${id}`);
            let parent = commandes.parentElement;
            parent.removeChild(commandes);
        } else {
            alert(`Impossible de supprimer le commandes selon le code d'identification ${id}`);
        }
    });
}
//--------------------------------------------
//              Promotions
//--------------------------------------------

function getFormulaireModifierPromotions(id) {

    fetch(host + "TableauDeBord/ModifierPromotions/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Le promotions est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#promotions-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierPromotions(id) {
    let parent = document.querySelector("#promotions-" + String(id)).querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/ModifierPromotions/", {
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
            });
        }
    });
}

function getFormulaireCreerPromotions() {

    fetch(host + "TableauDeBord/CreerPromotions/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerPromotions() {
    let parent = document.querySelector("#creer").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);

    fetch(host + "TableauDeBord/CreerPromotions/", {
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
                    let id = res.promotionsId;
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
            });
        }
    });
}

function supprimerPromotions(id) {
    fetch(host + "TableauDeBord/SupprimerPromotions/", {
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