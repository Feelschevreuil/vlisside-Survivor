function changerPage(numPage) {
    var conteneurBibli = document.getElementById("conteneurBibli");
    var conteneurEtu = document.getElementById("conteneurEtu");
    var inputRecherche = document.getElementById("inputRecherche");
    var RechercheSelection = document.getElementsByClassName("RechercheBiblio");
    var titreBibli = document.getElementById("titreBibli");
    var titreEtu = document.getElementById("titreEtu");
    var texteRecherche;
    var ouRecherche;
    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/../Recherche/RechercheRapide";
    var fetchEnLocalDuneAutrePage = "/../Recherche/RechercheRapideAutrePage"
    var fetchSurServeur = numeroEtudiant + "Recherche/RechercheRapide";
    var fetchSurServeurDuneAutrePage = fetchEnLocalDuneAutrePage + "Recherche/RechercheRapide";
    var stringFetch = "";
    var stringFetchAutrePage = "";
    var url = location.host;
    //var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value


    if (inputRecherche != null) {
        texteRecherche = inputRecherche.value
    }
    if (numPage == null) {
        numPage = 0;
    }

    if (url.match("localhost") == null) {
        stringFetch = fetchSurServeur;
        stringFetchAutrePage = fetchSurServeurDuneAutrePage;
    } else {
        stringFetch = fetchEnLocal;
        stringFetchAutrePage = fetchEnLocalDuneAutrePage;
    }

    if (RechercheSelection == null) {
        ouRecherche = 1; //bibli
    } else {
        if (RechercheSelection[1].selected) {
            ouRecherche = 2; // etu
        }
        else {
            ouRecherche = 1; // bibli
        }
    }

    var data = {
        numPage,
        texteRecherche,
        ouRecherche
    }

    data = JSON.stringify(data);

    if (conteneurBibli != null || conteneurEtu!=null) { // présentement ans la page
        fetch(stringFetch, {
            method: 'Post',
            body: data,
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-Type": "application/json",
                //"X-CSRF-TOKEN": csrfToken
            },

        }).then(function (response) {
            return response.text();

        }).then((data) => {

            if (conteneurBibli != null) {
                if (titreBibli != null) {
                    titreBibli.innerText = "Inventaire de la boutique étudiante"

                }
                conteneurBibli.innerHTML = data;
                RechercheSelection = document.getElementsByClassName("RechercheBiblio");
                RechercheSelection[1].selected = true;
                RechercheSelection[0].selected = false;
                conteneurBibli.id = "conteneurEtu"
                titreBibli.id = "titreEtu"
                NbLivrePanier()
            } else {
                if (titreEtu != null) {
                    titreEtu.innerText = "Inventaire de la bibliothèque"
                }
                conteneurEtu.innerHTML = data;
                RechercheSelection = document.getElementsByClassName("RechercheBiblio");
                RechercheSelection[0].selected = true;
                RechercheSelection[1].selected = false;
                conteneurEtu.id = "conteneurBibli"
                titreEtu.id = "titreBibli"
                NbLivrePanier()
            }
        });
    }
    else {
        fetch(stringFetchAutrePage, { // pas ans la page
            method: 'Post',
            body: data,
            contentType: "application/json; charset=utf-8",
            headers: {
                "Content-Type": "application/json",
                //"X-CSRF-TOKEN": csrfToken
            },

        }).then(function (response) {
            return response.text();

        }).then((data) => {

            document.body.innerHTML = data;
            NbLivrePanier()
        });
    }
}