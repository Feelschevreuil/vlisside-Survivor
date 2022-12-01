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

        if (livres.Neufs != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neufs[j].LivreId == id) {
                    if (etatLivre == "Neuf") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Neufs.splice(j, 1);
                    }
                }
            }
        }
        if (livres.Usages != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usages[j].LivreId == id) {
                    if (etatLivre == "Usagé") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Usages.splice(j, 1);
                    }
                }
            }
        }
        if (livres.Numeriques != null) {
            for (var j = 0; j < livres.Numeriques.length; j++) {
                if (livres.Numerique[j].LivreId == id) {
                    if (etatLivre == "Numérique") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Numeriques.splice(j, 1);
                    }
                }
            }
        }

    }
    else {
        switch (etatLivre) {
            case "Neuf":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [{ "LivreId": id, "Quantite": 1 }],
                    "Usages": [],
                    "Numeriques": []
                }))
                break;
            case "Usagé":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [],
                    "Usages": [{ "LivreId": id, "Quantite": 1 }],
                    "Numeriques": []
                }))
                break;
            case "Numerique":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [],
                    "Usages": [],
                    "Numeriques": [{ "LivreId": id, "Quantite": 1 }]
                }))
                break;
            default:
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [],
                    "Usages": [],
                    "Numeriques": []
                }))
        }
    }

    if (!idTrouveNonModifie) {
        switch (etatLivre) {
            case "Neuf":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Neufs.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Usagé":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Usages.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Numérique":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Numeriques.push(valeurLivre);
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

        if (livres.Neufs != null) {
            for (var j = 0; j < livres.Neufs.length; j++) {
                if (livres.Neufs[j].LivreId == id) {
                    livres.Neufs.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usages.length; j++) {
                if (livres.Usages[j].LivreId == id) {
                    livres.Usages.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numeriques.length; j++) {
                if (livres.Numeriques[j].LivreId == id) {
                    livres.Numeriques.splice(j, 1);
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

            if (livres.Neufs != null) {
                for (var j = 0; j < livres.Neufs.length; j++) {
                    if (livres.Neufs[j].LivreId == id) {
                        idTrouveModifie = true;
                        BoxChecked(id, "Neuf");
                    }
                }
            }
            if (livres.Usages != null) {
                for (var j = 0; j < livres.Usages.length; j++) {
                    if (livres.Usages[j].LivreId == id) {
                        idTrouveModifie = true;
                        BoxChecked(id, "Usager");
                    }
                }
            }
            if (livres.Numeriques != null) {
                for (var j = 0; j < livres.Numeriques.length; j++) {
                    if (livres.Numeriques[j].LivreId == id) {
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

            if (livres.Neufs != null) {
                for (var j = 0; j < livres.Neufs.length; j++) {
                    if (livres.Neufs[j].LivreId == id) {
                        livres.Neufs.splice(j, 1);
                    }
                }
            }
            if (livres.Usages != null) {
                for (var j = 0; j < livres.Usages.length; j++) {
                    if (livres.Usages[j].LivreId == id) {
                        livres.Usages.splice(j, 1);
                    }
                }
            }
            if (livres.Numeriques != null) {
                for (var j = 0; j < livres.Numeriques.length; j++) {
                    if (livres.Numeriques[j].LivreId == id) {
                        livres.Numeriques.splice(j, 1);
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

            if (livres.Neufs != null) {
                for (var i = 0; i < livres.Neufs.length; i++) {
                    nbLivreDansPanier += parseInt(livres.Neufs[i].Quantite)
                }
            }
            if (livres.Usages != null) {
                for (var i = 0; i < livres.Usages.length; i++) {
                    nbLivreDansPanier += parseInt(livres.Usages[i].Quantite)
                }
            }
            if (livres.Numeriques != null) {
                for (var i = 0; i < livres.Numeriques.length; i++) {
                    nbLivreDansPanier += parseInt(livres.Numeriques[i].Quantite)
                }
            }
            nbPanier.innerHTML = nbLivreDansPanier.toString();
        }
        else {
            nbPanier.innerHTML = "";
        }
    }
}
