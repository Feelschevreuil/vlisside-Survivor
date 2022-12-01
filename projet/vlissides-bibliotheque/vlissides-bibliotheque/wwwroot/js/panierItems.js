getCards()

function getCards() {
    var parentPartiel = document.querySelector("#affichageLivre");
    if (parentPartiel != null) {
        var div = document.querySelector("#cardsIci")
        const pImpossible = document.createElement("p");
        const pVide = document.createElement("p");
        pImpossible.innerHTML = 'Désolé, votre navigateur ne supporte pas le "localStorage". Essayez de changer de navigateur si ce problème persiste.'
        pVide.innerHTML = 'Votre panier est vide!'
        var tryStorage = true
        var tryEmpty = true
        var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
        var fetchEnLocal = "/../Panier/GetLivres";
        var fetchSurServeur = numeroEtudiant + "Panier/GetLivres";
        var stringFetch = "";
        var url = location.host;
        var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value
        var data = localStorage.getItem('itemsPanier');

        if (url.match("localhost") == null) {
            stringFetch = fetchSurServeur;
        } else {
            stringFetch = fetchEnLocal;
        }
        ;
        try {
            typeof (Storage) !== undefined
        }
        catch (e) {
            tryStorage = false
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
                    initQuantitePanier()
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
}

function updatePrix() {
    var Taxes = {
        "GST": 0.05,

    }
    var tousPrix = document.querySelectorAll('[id^="PrixLivreId"]');
    var tousQuantite = document.querySelectorAll('[id^="Quantite"]');
    var prixTotal = 0.00;
    for (var e = 0; e < tousPrix.length; e++) {
        prixCourant = GetDecimal(tousPrix[e].innerHTML.replace("&nbsp;","").replace("$","").replace(".",""))/100;
        prixTotal = (prixTotal + prixCourant * tousQuantite[e].value);
    }
    //parseFloat(tousPrix[e].innerHTML.replaceAll("$", ""));
    var pPrixSansTaxes = document.querySelector('#PrixAvantTaxes')
    var pPrixAvecTaxes = document.querySelector("#PrixAvecTaxes")
    var pTaxes = document.querySelector('#Taxes')
    if (prixTotal.toString().match(",") == null && prixTotal.toString().match(".") == null) {
        prixTotal = prixTotal + "." + 0 + 0
    }

    pTaxes.innerHTML = Taxes.GST * 100 + "%"
    pPrixSansTaxes.innerHTML = prixTotal.toFixed(2) + "$";
    pPrixAvecTaxes.innerHTML = (prixTotal + prixTotal * Taxes.GST).toFixed(2) + "$";
}

function GetDecimal(prix) {
    var prixAvecPoint = parseFloat(prix.toString().replace(",", "."));
    var prixEnDecimal = Number(prixAvecPoint.toString().match(/^\d+(?:\.\d{0,2})?/));
    return Number(prixEnDecimal);
}

function initQuantitePanier() {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'));

    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var i = 0; i < livres.Neuf.length; i++) {
                var quantite = document.getElementById("Quantite-" + livres.Neuf[i].LivreId)
                if (quantite != null) {
                    quantite.value = livres.Neuf[i].Quantite
                }
            }
        }
        if (livres.Usage != null) {
            for (var i = 0; i < livres.Usage.length; i++) {
                var quantite = document.getElementById("Quantite-" + livres.Usage[i].LivreId)
                if (quantite != null) {
                    quantite.value = livres.Usage[i].Quantite
                }
            }
        }
        if (livres.Numerique != null) {
            for (var i = 0; i < livres.Numerique.length; i++) {
                var quantite = document.getElementById("Quantite-" + livres.Numerique[i].LivreId)
                if (quantite != null) {
                    quantite.value = livres.Numerique[i].Quantite
                }
            }
        }
    }
}

function checkout() {
    var data = localStorage.getItem('itemsPanier');

    var stringFetch = "";
    var url = location.host;
    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/../Achat/Creer";
    var fetchSurServeur = numeroEtudiant + "Achat/Creer";
    if (url.match("localhost") == null) {
        stringFetch = fetchSurServeur;
    } else {
        stringFetch = fetchEnLocal;
    }

    var tryStorage = true
    try {
        typeof (Storage) !== undefined
    }
    catch (e) {
        tryStorage = false
    }

    var tryEmpty = true
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
                //todo

            }).then((data) => {
                //todo
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