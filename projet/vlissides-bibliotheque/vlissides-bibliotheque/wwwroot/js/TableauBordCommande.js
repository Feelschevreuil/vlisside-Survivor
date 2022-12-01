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

function afficherModificationCommande(id, data) {
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
            if (key == "Statut") {
                baliseInfo.innerHTML = data.NomStatut

            } else if (key == "DateFacturation") {
                baliseInfo.innerHTML = data.FormaterDateFacturation
            }else{
                baliseInfo.innerHTML = data[`${key}`];
            }
        }

    }
}

function afficherCreationCommande(id, data) {
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
            if (key == "Statut") {
                baliseInfo.innerHTML = data.NomStatut

            } else if (key == "DateFacturation") {
                baliseInfo.innerHTML = data.FormaterDateFacturation
            }else {
                baliseInfo.innerHTML = data[`${key}`];
            }
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

function creerBtnModifSuppriCommande(nouvelleLigne, id) {
    nouvelleLigne.classList.add("modif-suppr");
    trBtn = document.createElement("td");
    trBtn.classList.add("options-ligne", "position-absolute", "text-center", "vw-100", "start-0", "bg-transparent", "border-0")
    nouvelleLigne.appendChild(trBtn);

    livreImg = document.createElement("img");
    livreImg.classList.add("btn-img-hover");
    livreImg.setAttribute("onclick", 'getFormulaireListeLivres("'+ id +'")');
    livreImg.setAttribute("src", "/img/book-view.svg");
    trBtn.appendChild(livreImg);

    editImg = document.createElement("img")
    editImg.classList.add("btn-img-hover");
    editImg.setAttribute("data-bs-toggle", "modal");
    editImg.setAttribute("data-bs-target", "modal-modifier");
    editImg.setAttribute("onclick", 'getFormulaireModifierCommande("'+ id +'")');
    editImg.setAttribute("src", "/img/pencil.svg");
    trBtn.appendChild(editImg);

    deleteImg = document.createElement("img");
    deleteImg.classList.add("btn-img-hover");
    deleteImg.setAttribute("onclick", "supprimerCommande(" + id + ")");
    deleteImg.setAttribute("src", "/img/delete.svg");
    trBtn.appendChild(deleteImg);
    return nouvelleLigne;
}

function getListeCommandes() {
    var listcheckBox = document.querySelector("#afficherListLivres");
    if (listcheckBox.hidden == true) {
        listcheckBox.hidden = false
    } else {
        listcheckBox.hidden = true
    }
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

    fetch(host + "TableauDeBord/ModifierCommande/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("La commande est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#modal-modifier")
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function getFormulaireListeCommandes(id) {

    fetch(host + "TableauDeBord/AfficherListeCommandes/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("La liste de commandes associée est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#modal-commande")
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function ModifierCommande() {
    let parent = document.querySelector("#modal-modifier").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);
    data.Id = 0
    fetch(host + "TableauDeBord/ModifierCommande/", {
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
                    afficherModificationCommande(res.factureEtudiantId, resetMajusculeJsonKey(res));
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

    fetch(host + "TableauDeBord/CreerCommande/", {
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

    fetch(host + "TableauDeBord/CreerCommande/", {
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
                    let id = res.factureEtudiantId;
                    nouvelleLigne.id = "tr-" + id;
                    for (let i = 0; i < thead.children[0].childElementCount; i++) {
                        nouvelleLigne.appendChild(document.createElement("td"));
                    }
                    nouvelleLigne = creerBtnModifSuppriCommande(nouvelleLigne, id);
                    tbody.insertBefore(nouvelleLigne, tbody.children[0]);
                    afficherCreationCommande(id, resetMajusculeJsonKey(res));
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
    var confirmation = confirm("Êtes-vous sur de vouloir supprimer cette commande?");
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
                alert("La commande a supprimé avec succès!");
                let commade = document.querySelector(`#tr-${id}`);
                let parent = commade.parentElement;
                parent.removeChild(commade);
            } else {
                alert(`Impossible de supprimer la commande selon le code d'identification ${id}`);
            }
        });
    }
}