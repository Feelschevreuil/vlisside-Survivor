function getFormData(formulaire) {
    let formData = new FormData(formulaire);
    let data = {};

    formData.forEach(function (value, key) {
        let keys = key.split('.');
        let lastKey = keys.pop();
        let currentLevel = data;

        // Traverse the keys to create nested objects as needed
        keys.forEach(function (key) {
            if (!currentLevel[key]) {
                currentLevel[key] = {};
            }
            currentLevel = currentLevel[key];
        });

        // Parse JSON strings back into objects if possible
        try {
            value = JSON.parse(value);
        } catch (e) {
            // If it's not JSON, keep it as a string
        }

        // Handle array values for checkboxes
        if (currentLevel[lastKey]) {
            if (!Array.isArray(currentLevel[lastKey])) {
                currentLevel[lastKey] = [currentLevel[lastKey]];
            }
            currentLevel[lastKey].push(value);
        } else {
            // Check if the input is a checkbox
            if (formulaire.querySelector(`input[name="${key}"]`)?.type === 'checkbox') {
                currentLevel[lastKey] = [value];
            } else {
                currentLevel[lastKey] = value;
            }
        }
    });

    return data;
}


function validationLivre(DonnerRecus) {

    if (isNaN(DonnerRecus.MaisonDeditionId)) {
        DonnerRecus.MaisonDeditionId = 0
    }

    DonnerRecus.PrixNeuf ??= document.querySelector("#inputPrixNeuf").value;
    DonnerRecus.PrixNumerique ??= document.querySelector("#inputPrixNumerique").value;
    DonnerRecus.PrixUsage ??= document.querySelector("#inputPrixUsage").value;


    DonnerRecus.Prix.PrixNeuf = possedeDesLettres(DonnerRecus.PrixNeuf);
    DonnerRecus.Prix.PrixNumerique = possedeDesLettres(DonnerRecus.PrixNumerique);
    DonnerRecus.Prix.PrixUsage = possedeDesLettres(DonnerRecus.PrixUsage);
    DonnerRecus.Prix.QuantiteUsagee = possedeDesLettres(DonnerRecus.QuantiteUsagee);

    return DonnerRecus;
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

//-----------------------------------------------------
function assignerCoursEtudiant() {

    var DonnerRecus =
    {
        CoursId: getListCoursCocher()
    };

    var data = JSON.stringify(DonnerRecus);


    fetch(host + "GestionProfil/AssignerCoursEtudiant", {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {

        return response;

    });
}

function ajoutEditLivre() {
    var form = document.querySelector('#formLivre');
    var DonnerRecus = getFormData(form);

    DonnerRecus = validationLivre(DonnerRecus);

    debugger;

    DonnerRecus.ISBN = DonnerRecus.ISBN.toString();

    var data = JSON.stringify(DonnerRecus);

    fetch(host + "Inventaire/AjoutEditLivre", {
        method: 'Post',
        body: data,
        headers: {
            "Content-Type": "application/json; charset=utf-8",
        },

    }).then(function (response) {
        return response.text();

    }).then((data) => {
        var pageCourante = document.querySelector("#PageCourante");
        pageCourante.innerHTML = data;
    });
}

function getFormulaireCreerCours() {

    fetch(host + "TableauDeBord/CreerCours/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer-cours").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerCoursLivre() {
    let parent = document.querySelector("#creer-cours").querySelector(".modal-body");
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
                    var checkBoxRow = document.querySelector("#listDeCours").children[1].children[1];
                    var divCours = document.querySelector("#listDeCours").children[1];
                    var nouveauCheckBox = checkBoxRow.cloneNode(true);

                    nouveauCheckBox.children[0].id = res.id;
                    nouveauCheckBox.children[0].checked = true;
                    nouveauCheckBox.children[1].innerHTML = res.nom;
                    divCours.insertBefore(nouveauCheckBox, divCours.children[0]);
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

function getFormulaireCreerAuteurs() {

    fetch(host + "Inventaire/CreerAuteurs/", {
        method: 'GET',
    }).then(function (res) {
        if (res.ok) {
            res.text().then(function (res) {
                document.querySelector("#creer-auteurs").querySelector(".modal-body").innerHTML = res;
            });
        }
    });
}

function creerAuteursLivre() {
    let parent = document.querySelector("#creer-auteurs").querySelector(".modal-body");
    let formulaire = parent.querySelector("form");
    let data = getFormData(formulaire);
    data.Id = 0;

    fetch(host + "Inventaire/CreerAuteurs/", {
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
                    var checkBoxRow = document.querySelector("#listAuteurs").children[1].children[1];
                    var divCours = document.querySelector("#listAuteurs").children[1];
                    var nouveauCheckBox = checkBoxRow.cloneNode(true);

                    nouveauCheckBox.children[0].id = res.id;
                    nouveauCheckBox.children[0].checked = true;
                    nouveauCheckBox.children[1].innerHTML = res.nom;
                    divCours.insertBefore(nouveauCheckBox, divCours.children[0]);
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

