function assignerCours() {

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


    fetch("/GestionProfil/AssignerCours", {
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


function assignerCoursLivre() {

    var divListCours = document.querySelector("#listDeCours");
    var coursCocher = divListCours.querySelectorAll("input");
    var listCoursCocher = new Array();

    //TODO Peux remplacer ce qui a en dessous
    var form = document.querySelector('#formLivre');

    var Titre = document.querySelector("#Titre");
    var Resume = document.querySelector("#Resume");
    var Photo = document.querySelector("#viewImg");
    var DatePublication = document.querySelector("#DatePublication");
    var PrixUsage = document.querySelector("#PrixUsage");
    var PrixNumerique = document.querySelector("#PrixNumerique");
    var PrixNeuf = document.querySelector("#PrixNeuf");
    var QuantiteUsagee = document.querySelector("#QuantiteUsagee");
    var PossedeNumerique = document.querySelector("#PossedeNumerique");
    var PossedeNeuf = document.querySelector("#PossedeNeuf");
    var ISBN = document.querySelector("#ISBN");
    var AuteurId = document.querySelector("#AuteurId");
    var MaisonDeditionId = document.querySelector("#MaisonDeditionId");


    coursCocher.forEach((cours) => {
        if (cours.checked) {
            listCoursCocher.push(cours.id);
        }
    });

    var maisonEdition = MaisonDeditionId.value
    var autheur = AuteurId.value

    if (QuantiteUsagee.value == "") {
        QuantiteUsagee.value = 0;
    }
    if (AuteurId.value == "") {
        autheur = 0;
    }
    if (MaisonDeditionId.value == "") {

        maisonEdition = 0
    }

    var DonnerRecus =
    {
        CoursId: listCoursCocher,
        Titre: Titre.value,
        Resume: Resume.value,
        Photo: Photo.src,
        DatePublication: DatePublication.value,
        PrixUsage: PrixUsage.value,
        PrixNumerique: PrixNumerique.value,
        PrixNeuf: PrixNeuf.value,
        QuantiteUsagee: QuantiteUsagee.value,
        PossedeNumerique: PossedeNumerique.checked,
        PossedeNeuf: PossedeNeuf.checked,
        ISBN: ISBN.value,
        AuteurId: autheur,
        MaisonDeditionId: maisonEdition,
        
    }

    var data = JSON.stringify(DonnerRecus);


    fetch("/Inventaire/Creer", {
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