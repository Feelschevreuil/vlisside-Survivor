function changerPage(numPage) {
    var conteneurBibli = document.getElementById("conteneurBibli");
    var inputRecherche = document.getElementById("inputRecherche");
    var texteRecherche;
    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/../Recherche/RechercheRapide";
    var fetchSurServeur = numeroEtudiant + "Recherche/RechercheRapide";
    var stringFetch = "";
    var url = location.host;
    var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value
    

    if (inputRecherche != null) {
        texteRecherche=inputRecherche.value
    }
    if (numPage==null) {
        numPage = 0;
    }

    if (url.match("localhost") == null) {
        stringFetch = fetchSurServeur;
    } else {
        stringFetch = fetchEnLocal;
    }

    var data = {
        numPage,
        texteRecherche
    }

    data=JSON.stringify(data);

    if (conteneurBibli != null) {
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
        });
    }
}