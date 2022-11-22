function getFormData(formulaire) {
    let formData = new FormData(formulaire);
    let data = {};

    formData.forEach(function (value, key) {
        data[key] = value;
    });

    return data;
}

//-----------------------------------------------------
function assignerCoursEtudiant() {

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
        CoursId: listCoursCocher
    }; 

    var data = JSON.stringify(DonnerRecus);

    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/GestionProfil/AssignerCoursEtudiant";
    var fetchSurServeur = numeroEtudiant + "GestionProfil/AssignerCoursEtudiant";
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


function assignerCoursLivre() {

    var divListCours = document.querySelector("#listDeCours");
    var coursCocher = divListCours.querySelectorAll("input");
    var listCoursCocher = new Array();

    //TODO Peux remplacer ce qui a en dessous
    var form = document.querySelector('#formLivre');
    var DonnerRecus = getFormData(form);

    if (DonnerRecus.AuteurId.value == "") {
        DonnerRecus.AuteurId.value = 0;
    }
    if (DonnerRecus.MaisonDeditionId.value == "")
    {
        DonnerRecus.MaisonDeditionId.value = 0
    }
    if (DonnerRecus.PrixNeuf == null || DonnerRecus.PrixNeuf == undefined)
    {
        DonnerRecus.PrixNeuf = document.querySelector("#inputPrixNeuf").value;
    }
    if (DonnerRecus.PrixNumerique == null || DonnerRecus.PrixNumerique == undefined)
    {
        DonnerRecus.PrixNumerique = document.querySelector("#inputPrixNumerique").value;
    }
    if (DonnerRecus.PrixUsage == null || DonnerRecus.PrixUsage == undefined)
    {
        DonnerRecus.PrixUsage = document.querySelector("#inputPrixUsage").value
    }


    coursCocher.forEach((cours) => {
        if (cours.checked) {
            listCoursCocher.push(cours.id);
        }
    });

    DonnerRecus.CoursId = listCoursCocher;
    DonnerRecus.PrixNeuf = possedeDesLettres(DonnerRecus.PrixNeuf);
    DonnerRecus.PrixNumerique = possedeDesLettres(DonnerRecus.PrixNumerique);
    DonnerRecus.PrixUsage = possedeDesLettres(DonnerRecus.PrixUsage);
    DonnerRecus.QuantiteUsagee = possedeDesLettres(DonnerRecus.QuantiteUsagee);
    DonnerRecus.PossedeNeuf = PossedeNeuf.checked;
    DonnerRecus.PossedeNumerique= PossedeNumerique.checked;
    DonnerRecus.PossedeUsagee = PossedeUsagee.checked;

    var data = JSON.stringify(DonnerRecus);

    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/Inventaire/Creer";
    var fetchSurServeur = numeroEtudiant + "Inventaire/Creer";
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