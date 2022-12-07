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
        var csrfToken = document.getElementsByName("__RequestVerificationToken")[0].value
        var data = localStorage.getItem('itemsPanier');

        try {
            typeof (Storage) !== undefined
        }
        catch (e) {
            tryStorage = false
        }

        try {
            if (JSON.parse(localStorage.getItem('itemsPanier')).Neufs == undefined && JSON.parse(localStorage.getItem('itemsPanier')).Usages == undefined && JSON.parse(localStorage.getItem('itemsPanier')).Numeriques == undefined)
                tryEmpty = false
        }
        catch (e) {
            tryEmpty = false
        }

        if (tryStorage) {
            if (tryEmpty) {

                fetch(host + "Panier/GetLivres", {
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

        if (livres.Neufs != null) {
            for (var i = 0; i < livres.Neufs.length; i++) {
                var quantite = document.getElementById("Quantite-" + livres.Neufs[i].LivreId)
                if (quantite != null) {
                    quantite.value = livres.Neufs[i].Quantite
                }
            }
        }
        if (livres.Usages != null) {
            for (var i = 0; i < livres.Usages.length; i++) {
                var quantite = document.getElementById("Quantite-" + livres.Usages[i].LivreId)
                if (quantite != null) {
                    quantite.value = livres.Usages[i].Quantite
                }
            }
        }
        if (livres.Numeriques != null) {
            for (var i = 0; i < livres.Numeriques.length; i++) {
                var quantite = document.getElementById("Quantite-" + livres.Numeriques[i].LivreId)
                if (quantite != null) {
                    quantite.value = livres.Numeriques[i].Quantite
                }
            }
        }
    }
}

function checkout() {
    var data = localStorage.getItem('itemsPanier');

    var tryStorage = true
    try {
        typeof (Storage) !== undefined
    }
    catch (e) {
        tryStorage = false
    }

    var tryEmpty = true
    try {
        if (JSON.parse(localStorage.getItem('itemsPanier')).Neufs == undefined && JSON.parse(localStorage.getItem('itemsPanier')).Usages == undefined && JSON.parse(localStorage.getItem('itemsPanier')).Numeriques == undefined)
            tryEmpty = false
    }
    catch (e) {
        tryEmpty = false
    }

    if (tryStorage) {
        if (tryEmpty) {
            fetch(host + "Achat/Creer/", {
                method: 'Post',
                body: data,
                contentType: "application/json; charset=utf-8",
                headers: {
                    "Content-Type": "application/json",
                    //"X-CSRF-TOKEN": csrfToken
                },
            }).then(async response => {

                if(response.ok) {

                    window.location = host + "Achat/" + "?id=" + await response.text();
                }
                else {

                    alert("Une erreur est survenue!");
                }

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
