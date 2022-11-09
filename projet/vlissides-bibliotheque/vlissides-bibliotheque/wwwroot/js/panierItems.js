function getCards() {
    var div = document.getElementById("cardsIci")
    const pImpossible = document.createElement("p");
    const pVide = document.createElement("p");
    pImpossible.innerHTML = 'Désolé, votre navigateur ne supporte pas le "localStorage". Essayez de changer de navigateur si ce problème persiste.'
    pVide.innerHTML = 'Votre panier est vide!'
    var tryStorage = true
    var tryEmpty = true

    //fausse valeurs
    var ids = ["51", "34"]
    //var ids = []
    var objetLocalStorage = { "id": ids };
    localStorage.setItem('itemsPanier', JSON.stringify(objetLocalStorage))

    var fetchEnLocal = "/../Panier/GetLivres";
    var fetchSurServeur = "2036516/Panier/GetItems";
    var stringFetch = "";
    var url = location.host;
    var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value
    var data = (localStorage.getItem('itemsPanier'));

    if (url.match("localhost") == null) {
        stringFetch = fetchSurServeur;
    } else {
        stringFetch = fetchEnLocal;
    }
;
    try {
        typeof (Storage) !== undefined
    }
    catch(e) {
        tryStorage=false
    }

    try {
        if (JSON.parse(localStorage.getItem('itemsPanier')).id[0] == undefined)
            tryEmpty = false
    }
    catch (e) {
        tryEmpty = false
    }

    if (tryStorage) {
        if (tryEmpty) {

            fetch(stringFetch, {
                method: 'Post',
                body: data,
                contentType: "application/json; charset=utf-8",
                headers: {
                    "Content-Type": "application/json",
                    //"X-CSRF-TOKEN": csrfToken
                },

            }).then(function (response) {

                return response.text;

            }).then((data) => {

                var parentPartiel = document.getElementById("affichageLivre");
                parentPartiel.html(data);

            });


        }
        else {
            div.appendChild(pVide);
        }
    }
    else {
        div.appendChild(pImpossible);
    }
}