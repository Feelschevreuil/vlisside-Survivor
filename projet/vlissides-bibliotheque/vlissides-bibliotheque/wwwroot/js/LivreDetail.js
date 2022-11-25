NbLivrePanier()
initDetail()

function BoxChecked(LivreId, etat) {
    var boutonPanierSupprimer = document.getElementById("supprimer")
    if (boutonPanierSupprimer.hidden) {
        var baliseEtat = document.querySelector('#' + etat + "-" + LivreId);
        var parent = baliseEtat.parentElement;
        for (let input of parent.querySelectorAll("label")) {
            input.classList.remove("bg-back");
            input.classList.add("bg-top");

        };
        baliseEtat.classList.add("bg-back");
        baliseEtat.classList.remove("bg-top");
    }
}

function CopierCourriel() {

    var courriel = document.querySelector("#Courriel");

    courriel.select();
    courriel.setSelectionRange(0, 99999); // Pour téléphone

    navigator.clipboard.writeText(courriel.value);
}

function ajouterLivreLocalStorage(id) {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))
    var idTrouveNonModifie = false;
    var updateLocalStorage = true;
    var etatLivre = document.getElementsByClassName("bg-back")[0].innerText
    var p = document.getElementById("pErreur");
    var supprimer = document.getElementById("supprimer")
    var ajouter = document.getElementById("ajouter")
    p.innerText = "";

    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neuf[j].LivreId == id) {
                    if (etatLivre == "Neuf") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Neuf.splice(j, 1);
                    }
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usage[j].LivreId == id) {
                    if (etatLivre == "Usagé") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Usage.splice(j, 1);
                    }
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                if (livres.Numerique[j].LivreId == id) {
                    if (etatLivre == "Numérique") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Numerique.splice(j, 1);
                    }
                }
            }
        }

    }
    else {
        switch (etatLivre) {
            case "Neuf":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neuf": [{ "LivreId": id, "Quantite": 1 }],
                    "Usage": [],
                    "Numerique": []
                }))
                break;
            case "Usagé":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neuf": [],
                    "Usage": [{ "LivreId": id, "Quantite": 1 }],
                    "Numerique": []
                }))
                break;
            case "Numerique":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neuf": [],
                    "Usage": [],
                    "Numerique": [{ "LivreId": id, "Quantite": 1 }]
                }))
                break;
            default:
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neuf": [],
                    "Usage": [],
                    "Numerique": []
                }))
        }
    }

    if (!idTrouveNonModifie) {
        switch (etatLivre) {
            case "Neuf":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Neuf.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Usagé":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Usage.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Numérique":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Numerique.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            default:
                updateLocalStorage = false;
        }
    }
    else {
        var p = document.getElementById("pErreur");
        p.innerText = "* Ce livre se trouve déjà dans votre panier";
        ajouter.hidden = true;
        supprimer.hidden = false;
    }

    if (!updateLocalStorage) {
        var p = document.getElementById("pErreur");
        p.innerText = "* L'état du livre que vous essayez d'ajouter à votre panier est invalide, sélectionnez un autre état."
    }

    NbLivrePanier()
}

function supprimerLivreLocalStorage(id) {
    var supprimer = document.getElementById("supprimer")
    var ajouter = document.getElementById("ajouter")
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))
    var idTrouveModifie = false;
    var p = document.getElementById("pErreur");
    p.innerText = "";


    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neuf[j].LivreId == id) {
                    livres.Neuf.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usage[j].LivreId == id) {
                    livres.Usage.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                if (livres.Numerique[j].LivreId == id) {
                    livres.Numerique.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }

    }

    if (idTrouveModifie) {
        localStorage.setItem('itemsPanier', JSON.stringify(livres))
        ajouter.hidden = false;
        supprimer.hidden = true;
    }
    else {
        p.innerText = "Erreur lors de la suppression du livre"
    }

    NbLivrePanier()
}

function initDetail() {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'));
    var supprimer = document.getElementById("supprimer");
    var ajouter = document.getElementById("ajouter");
    var idLivre = document.getElementById("idLivre");
    if (idLivre != null) {
        var id = idLivre.innerText;
        var idTrouveModifie = false;
    }

    if (idLivre != null) {
        if (livres != null || livres != undefined) {

            if (livres.Neuf != null) {
                for (var j = 0; j < livres.Neuf.length; j++) {
                    if (livres.Neuf[j].LivreId == id) {
                        idTrouveModifie = true;
                        BoxChecked(id, "Neuf");
                    }
                }
            }
            if (livres.Usage != null) {
                for (var j = 0; j < livres.Usage.length; j++) {
                    if (livres.Usage[j].LivreId == id) {
                        idTrouveModifie = true;
                        BoxChecked(id, "Usager");
                    }
                }
            }
            if (livres.Numerique != null) {
                for (var j = 0; j < livres.Numerique.length; j++) {
                    if (livres.Numerique[j].LivreId == id) {
                        idTrouveModifie = true;
                        BoxChecked(id, "Numerique");
                    }
                }
            }

        }

        if (idTrouveModifie) {
            supprimer.hidden = false;
            ajouter.hidden = true;
        }
        else {
            document.getElementById("ajouter").hidden = true
            document.getElementById("supprimer").hidden = true
        }
    }
}

function supresionRapide(id) {
    let confirmtaion = confirm("Cet item sera retiré du panier")
    if (confirmtaion) {
        var livres = JSON.parse(localStorage.getItem('itemsPanier'))

        if (livres != null || livres != undefined) {

            if (livres.Neuf != null) {
                for (var j = 0; j < livres.Neuf.length; j++) {
                    if (livres.Neuf[j].LivreId == id) {
                        livres.Neuf.splice(j, 1);
                    }
                }
            }
            if (livres.Usage != null) {
                for (var j = 0; j < livres.Usage.length; j++) {
                    if (livres.Usage[j].LivreId == id) {
                        livres.Usage.splice(j, 1);
                    }
                }
            }
            if (livres.Numerique != null) {
                for (var j = 0; j < livres.Numerique.length; j++) {
                    if (livres.Numerique[j].LivreId == id) {
                        livres.Numerique.splice(j, 1);
                    }
                }
            }

            localStorage.setItem('itemsPanier', JSON.stringify(livres))
            location.reload()
        }
    }
}

function NbLivrePanier() {
    var nbPanier = document.getElementById("NbItemPanier");
    if (nbPanier != null) {
        var livres = JSON.parse(localStorage.getItem('itemsPanier'));
        var nbLivreDansPanier = 0;

        if (livres != null || livres != undefined) {

            if (livres.Neuf != null) {
                for (var i = 0; i < livres.Neuf.length; i++) {
                    nbLivreDansPanier += parseInt(livres.Neuf[i].Quantite)
                }
            }
            if (livres.Usage != null) {
                for (var i = 0; i < livres.Usage.length; i++) {
                    nbLivreDansPanier += parseInt(livres.Usage[i].Quantite)
                }
            }
            if (livres.Numerique != null) {
                for (var i = 0; i < livres.Numerique.length; i++) {
                    nbLivreDansPanier += parseInt(livres.Numerique[i].Quantite)
                }
            }
            nbPanier.innerHTML = nbLivreDansPanier.toString();
        }
        else {
            nbPanier.innerHTML = "";
        }
    }
}
