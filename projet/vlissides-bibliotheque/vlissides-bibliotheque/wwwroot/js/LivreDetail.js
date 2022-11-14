function BoxChecked(LivreId, etat) {
    var baliseEtat = document.querySelector('#' + etat + "-" + LivreId);
    var parent = baliseEtat.parentElement;
    for (let input of parent.querySelectorAll("label")) {
        input.classList.remove("bg-back");
        input.classList.add("bg-top");

    };
    baliseEtat.classList.add("bg-back");
    baliseEtat.classList.remove("bg-top");
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
    p.innerText = "";

    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neuf[j] == id) {
                    if (etatLivre == "Neuf") {
                        idTrouveNonModifie = true;
                    }
                    else {
                        livres.Neuf.splice(j,1);
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
                break;
            case "Usagé":
                livres.Usage.push(id.toString());
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                break;
            case "Numérique":
                livres.Numerique.push(id.toString());
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                updateLocalStorage = true;
                break;
            default:
                updateLocalStorage = false;
        }
    }
    else {
        var p = document.getElementById("pErreur");
        p.innerText = "* Ce livre se trouve déjà dans votre panier";
    }

    if (!updateLocalStorage) {
        var p = document.getElementById("pErreur");
        p.innerText = "* L'état du livre que vous essayez d'ajouter à votre panier est invalide, sélectionnez un autre état."
    }
}