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

function afficherModificationPromotion(id, data) {
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
            } else if (key == "Debut") {
                baliseInfo.innerHTML = data.DateDebutFormatter;
            } else if (key == "Fin") {
                baliseInfo.innerHTML = data.DateFinFormatter;
            } else {
                baliseInfo.innerHTML = data[`${key}`];
            }
        }

    }
}

function afficherCreationPromotion(id, data) {
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
            } else if (key == "Debut") {
                baliseInfo.innerHTML = data.DateDebutFormatter;
            } else if (key == "Fin") {
                baliseInfo.innerHTML = data.DateFinFormatter;
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

function possedeDesLettres(nombre) {
    if (isNaN(parseFloat(nombre)) || nombre == "") {
        return nombre = 0;
    }
    else {
        return parseFloat(nombre.replace(",", "."));
    }
}

function creerBtnModifSuppriPromotion(nouvelleLigne, id) {
    nouvelleLigne.classList.add("modif-suppr");
    trBtn = document.createElement("td");
    trBtn.classList.add("options-ligne", "position-absolute", "text-center", "vw-100", "start-0", "bg-transparent", "border-0")
    nouvelleLigne.appendChild(trBtn);
    editImg = document.createElement("img")
    editImg.classList.add("btn-img-hover");
    editImg.setAttribute("data-bs-toggle", "modal");
    editImg.setAttribute("data-bs-target", "#modal-modifier");
    editImg.setAttribute("onclick", "getFormulaireModifierPromotion(" + id + ")");
    editImg.setAttribute("src", "/img/pencil.svg");
    trBtn.appendChild(editImg);

    deleteImg = document.createElement("img");
    deleteImg.classList.add("btn-img-hover");
    deleteImg.setAttribute("onclick", "supprimerPromotion(" + id + ")");
    deleteImg.setAttribute("src", "/img/delete.svg");
    trBtn.appendChild(deleteImg);
    return nouvelleLigne;
}

//--------------------------------------------
//              Promotions
//--------------------------------------------
function getPartialViewPromotions() {
    fetch(host + "TableauDeBord/Promotions/", {
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

function getFormulaireModifierPromotion(id) {

    fetch(host + "TableauDeBord/ModifierPromotions/" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("La promotion est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#modal-modifier")
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierPromotions() {
    let parent = document.querySelector("#modal-modifier").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);
    data.Id = 0
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
                    afficherModificationPromotion(res.id, resetMajusculeJsonKey(res));
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

function getFormulaireCreerPromotion() {

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

function creerPromotion() {
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
                    let id = res.id;
                    nouvelleLigne.id = "tr-" + id;
                    for (let i = 0; i < thead.children[0].childElementCount; i++) {
                        nouvelleLigne.appendChild(document.createElement("td"));
                    }
                    nouvelleLigne = creerBtnModifSuppriPromotion(nouvelleLigne, id);
                    tbody.insertBefore(nouvelleLigne, tbody.children[0]);
                    afficherCreationPromotion(id, resetMajusculeJsonKey(res));
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

function supprimerPromotion(id) {
    var confirmation = confirm("Êtes-vous sur de vouloir supprimer cet évênement?");
    if (confirmation) {
        fetch(host + "TableauDeBord/SupprimerPromotion/", {
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
                alert(`Impossible de supprimer la promotion selon le code d'identification ${id}`);
            }
        });
    }
}