function getFormData(formulaire) {
    let formData = new FormData(formulaire);
    let data = {};

    formData.forEach(function (value, key) {
        data[key] = value;
    });

    return data;
}

function getListCoursCocher() {
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

function getListAuteursCocher() {
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

function gestionErreurLivre(DonnerRecus) {

    if (DonnerRecus.MaisonDeditionId == "") {
        DonnerRecus.MaisonDeditionId = 0
    }
    if (DonnerRecus.PrixNeuf == null || DonnerRecus.PrixNeuf == undefined) {
        DonnerRecus.PrixNeuf = document.querySelector("#inputPrixNeuf").value;
    }
    if (DonnerRecus.PrixNumerique == null || DonnerRecus.PrixNumerique == undefined) {
        DonnerRecus.PrixNumerique = document.querySelector("#inputPrixNumerique").value;
    }
    if (DonnerRecus.PrixUsage == null || DonnerRecus.PrixUsage == undefined) {
        DonnerRecus.PrixUsage = document.querySelector("#inputPrixUsage").value
    }
    DonnerRecus.PrixNeuf = possedeDesLettres(DonnerRecus.PrixNeuf);
    DonnerRecus.PrixNumerique = possedeDesLettres(DonnerRecus.PrixNumerique);
    DonnerRecus.PrixUsage = possedeDesLettres(DonnerRecus.PrixUsage);
    DonnerRecus.QuantiteUsagee = possedeDesLettres(DonnerRecus.QuantiteUsagee);
    DonnerRecus.PossedeNeuf = PossedeNeuf.checked;
    DonnerRecus.PossedeNumerique = PossedeNumerique.checked;
    DonnerRecus.PossedeUsagee = PossedeUsagee.checked;

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
        CoursId: getListCoursCocher(),
        AuteursId
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

function modifierCoursLivre(id) {

    var divListCours = document.querySelector("#listDeCours");
    var coursCocher = divListCours.querySelectorAll("input");
    var listCoursCocher = new Array();

    coursCocher.forEach((cours) => {
        if (cours.checked) {
            listCoursCocher.push(cours.id);
        }
    });

    var DonnerRecus =
    {
        CoursId: listCoursCocher,
        livreId: id
    };

    var data = JSON.stringify(DonnerRecus);

    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/Inventaire/AssignerCoursLivre";
    var fetchSurServeur = numeroEtudiant + "Inventaire/AssignerCoursLivre";
    var stringFetch = "";
    var url = location.host;


    if (url.match("localhost") == null) {
        stringFetch = fetchSurServeur;
    } else {
        stringFetch = fetchEnLocal;
    }


    fetch(stringFetch, {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {
        if (response.ok) {
            alert("Vos cours ont été modifiés avec succès");
        }
        return response;

    });
}

function modifierAuteursLivre(id) {

    var DonnerRecus =
    {
        AuteursId: getListAuteursCocher(),
        livreId: id
    };

    var data = JSON.stringify(DonnerRecus);

    fetch(host + "Inventaire/AssignerAuteursLivre", {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {
        if (response.ok) {
            alert("Vos auteurs ont été modifiés avec succès");
        }
        return response;

    });
}

function assignerCoursLivre() {
    var form = document.querySelector('#formLivre');
    var DonnerRecus = getFormData(form);

    DonnerRecus = gestionErreurLivre(DonnerRecus);
    DonnerRecus.Cours = getListCoursCocher();
    DonnerRecus.Auteurs = getListAuteursCocher();

    var data = JSON.stringify(DonnerRecus);

    fetch(host + "Inventaire/Creer", {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
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