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
                if (livres.Neuf[j] == id) {
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
                if (livres.Usage[j] == id) {
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
                if (livres.Numerique[j] == id) {
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
                    "Neuf": [id],
                    "Usage": [],
                    "Numerique": []
                }))
                break;
            case "Usagé":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neuf": [],
                    "Usage": [id],
                    "Numerique": []
                }))
                break;
            case "Numerique":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neuf": [],
                    "Usage": [],
                    "Numerique": [id]
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
                livres.Neuf.push(id.toString());
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Usagé":
                livres.Usage.push(id.toString());
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Numérique":
                livres.Numerique.push(id.toString());
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
                if (livres.Neuf[j] == id) {
                    livres.Neuf.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usage[j] == id) {
                    livres.Usage.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                if (livres.Numerique[j] == id) {
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

}

function initDetail() {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'));
    var supprimer = document.getElementById("supprimer");
    var ajouter = document.getElementById("ajouter");
    var idLivre = document.getElementById("idLivre");
    var id = idLivre.innerText;


    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neuf[j] == id) {
                    idTrouveModifie = true;
                    BoxChecked(id, "Neuf");
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usage[j] == id) {
                    idTrouveModifie = true;
                    BoxChecked(id, "Usager");
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                if (livres.Numerique[j] == id) {
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
}

function supresionRapide(id) {
    let confirmtaion = confirm("Cet item sera retiré du panier")
    if (confirmtaion) {
        var livres = JSON.parse(localStorage.getItem('itemsPanier'))

        if (livres != null || livres != undefined) {

            if (livres.Neuf != null) {
                for (var j = 0; j < livres.Neuf.length; j++) {
                    if (livres.Neuf[j] == id) {
                        livres.Neuf.splice(j, 1);
                    }
                }
            }
            if (livres.Usage != null) {
                for (var j = 0; j < livres.Usage.length; j++) {
                    if (livres.Usage[j] == id) {
                        livres.Usage.splice(j, 1);
                    }
                }
            }
            if (livres.Numerique != null) {
                for (var j = 0; j < livres.Numerique.length; j++) {
                    if (livres.Numerique[j] == id) {
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
    var livres = JSON.parse(localStorage.getItem('itemsPanier'));
    var nbLivreDansPanier = 0;

    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            nbLivreDansPanier += livres.Neuf.length
        }
        if (livres.Usage != null) {
            nbLivreDansPanier += livres.Usage.length
        }
        if (livres.Numerique != null) {
            nbLivreDansPanier += livres.Numerique.length
        }
        nbPanier.innerHTML = nbLivreDansPanier.toString();
    }
    else {
        nbPanier.innerHTML = "";
    }
}
