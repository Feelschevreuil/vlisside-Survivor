function changerPage(numPage) {
    var conteneurBibli = document.getElementById("conteneurBibli");
    var inputRecherche = document.getElementById("inputRecherche");
    var RechercheSelection = document.getElementsByClassName("RechercheBiblio");
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

    if (conteneurBibli != null) { // présentement ans la page
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

            conteneurBibli.innerHTML = data;
            NbLivrePanier()
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