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


function possedeDesLettres(nombre) {

    
    if (isNaN(parseFloat(nombre)) || nombre == "")
    {
        return nombre = 0;
    }
    else
    {
        return parseFloat(nombre.replace(",", ".")); 
    }
}