getCards()

function getCards() {
    var div = document.querySelector("#cardsIci")
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
    //var objetLocalStorage =
    //{
    //    "Neuf":
    //    [
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        },
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        },
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        }
    //    ],
    //        "Numerique":
    //    [
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        },
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        },
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        }
    //    ],
    //        "Usage":
    //    [
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        },
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        },
    //        {
    //            "LivreId": "id",
    //            "Quantite": "666"
    //        }
    //    ]
    //}
    
    var numeroEtudiant = "/"+window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/../Panier/GetLivres";
    var fetchSurServeur = numeroEtudiant+"Panier/GetLivres";
    var stringFetch = "";
    var url = location.host;
    var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value
    var data = localStorage.getItem('itemsPanier');
    var parentPartiel = document.querySelector("#affichageLivre");

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
        "GST": 0.05,
       
    }
    var tousPrix = document.querySelectorAll('[id^="PrixLivreId"]');
    var prixTotal = 0.00;
    for (var e = 0; e < tousPrix.length; e++) {
        prixCourant = GetDecimal(tousPrix[e].innerHTML);
        prixTotal = prixTotal + prixCourant;
    }
    //parseFloat(tousPrix[e].innerHTML.replaceAll("$", ""));
    var pPrixSansTaxes = document.querySelector('#PrixAvantTaxes')
    var pPrixAvecTaxes = document.querySelector("#PrixAvecTaxes")
    if (prixTotal.toString().match(",") == null && prixTotal.toString().match(".") == null) {
        prixTotal = prixTotal + "." + 0 + 0
    }
    

    pPrixSansTaxes.innerHTML = prixTotal.toFixed(2) + "$";
    pPrixAvecTaxes.innerHTML = (prixTotal + prixTotal * Taxes.GST).toFixed(2) + "$";
}

function GetDecimal(prix) {
    var prixAvecPoint = parseFloat(prix.toString().replace(",", "."));
    var prixEnDecimal = Number(prixAvecPoint.toString().match(/^\d+(?:\.\d{0,2})?/));
    return Number(prixEnDecimal);
}