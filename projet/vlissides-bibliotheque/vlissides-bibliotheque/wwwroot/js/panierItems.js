getCards()

function getCards() {
    var div = document.getElementById("cardsIci")
    const pImpossible = document.createElement("p");
    const pVide = document.createElement("p");
    pImpossible.innerHTML = 'Désolé, votre navigateur ne supporte pas le "localStorage". Essayez de changer de navigateur si ce problème persiste.'
    pVide.innerHTML = 'Votre panier est vide!'
    var tryStorage = true
    var tryEmpty = true

    //fausse valeurs tests
    //var ids=[]
    //var objetLocalStorage =
    //{
    //        "Neuf": ["51"],
    //        "Usage": ["64", "67", "68"],
    //        "Numerique": [ "65", "66"]
    //}
    //localStorage.setItem('itemsPanier', JSON.stringify(objetLocalStorage))

    var fetchEnLocal = "/../Panier/GetLivres";
    var fetchSurServeur = "2036516/Panier/GetItems";
    var stringFetch = "";
    var url = location.host;
    var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value
    var data = localStorage.getItem('itemsPanier');
    var parentPartiel = document.getElementById("affichageLivre");

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
        if (JSON.parse(localStorage.getItem('itemsPanier')).Neuf == undefined && JSON.parse(localStorage.getItem('itemsPanier')).Usage == undefined && JSON.parse(localStorage.getItem('itemsPanier')).Numerique == undefined)
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
                return response.text();

            }).then((data) => {

                parentPartiel.innerHTML = data;
                updatePrix();
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

function updatePrix() {
    var Taxes = {
        "TPS": 0.05,
        "TVQ":0.09975
    }
    var tousPrix = document.querySelectorAll('[id^="PrixLivreId"]');
    var prixTotal = 0.0;
    for (var e = 0; e < tousPrix.length; e++) {
        prixTotal += parseFloat(tousPrix[e].innerHTML.replaceAll("$", ""));
    }

    var pPrixSansTaxes = document.getElementById('PrixAvantTaxes')
    var pPrixAvecTaxes = document.getElementById("PrixAvecTaxes")

    pPrixSansTaxes.innerHTML = prixTotal.toFixed(2) + "$";
    pPrixAvecTaxes.innerHTML = (prixTotal + prixTotal * Taxes.TPS + prixTotal * Taxes.TVQ).toFixed(2) + "$";
}