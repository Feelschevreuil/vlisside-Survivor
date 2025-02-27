function ChangerPrix(Etat, Id) {

    var DonnerRecus =
    {
        etat: Etat,
        id: Id,
       
    };
    var cercle = document.querySelector("#cercleJaune" + "N-" + DonnerRecus.id);
    var PrixLoading = document.querySelector('#' + "PrixLivreId" + "-" + DonnerRecus.id);
    var SpinnerLoading = document.querySelector('#' + "Spinner" + "-" + DonnerRecus.id);

    PrixLoading.hidden = true;
    SpinnerLoading.hidden = false;
    
    var data = JSON.stringify(DonnerRecus);

    fetch(host + "Accueil/ChangerPrix", {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {

        return response.json();

    }).then((data) => {

        SpinnerLoading.hidden = true;
        PrixLoading.hidden = false;

        var balisePrix = document.querySelector('#' + "PrixLivreId" + "-" + data.Id);
        balisePrix.innerHTML = data.prix + "$";
        
     });

}

function checkTheBox(LivreId,etat) {
    var baliseEtat = document.querySelector('#' + etat + "-" + LivreId);
    var parent = baliseEtat.parentElement;
    for (let input of parent.querySelectorAll("label")) {
        input.classList.remove("bg-back");
        input.classList.add("bg-top");
       
    };
    baliseEtat.classList.add("bg-back");
    baliseEtat.classList.remove("bg-top");

}

function ajoutRapide(id) {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))
    var idTrouveNonModifie = false;
    var boutonNeuf = document.getElementById("Neuf-" + id);
    var boutonUsager = document.getElementById("Usager-" + id);
    var boutonNumerique = document.getElementById("Numerique-" + id);
    var etatLivre = ""
    var supprimer = document.getElementById("suppressionRapideBibli-"+id)
    var ajouter = document.getElementById("ajoutRapide-"+id)

    if(boutonNeuf!=null)
        if (boutonNeuf.classList.contains("bg-back"))
            etatLivre = "Neuf";
    if (boutonNumerique != null)
        if (boutonNumerique.classList.contains("bg-back"))
            etatLivre = "Numérique";
    if (boutonUsager != null)
        if (boutonUsager.classList.contains("bg-back"))
            etatLivre = "Usagé";


    if (livres != null || livres != undefined) {

        if (livres.Neufs != null) {
            for (var j = 0; j < livres.Neufs.length; j++) {
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
            for (var j = 0; j < livres.Usages.length; j++) {
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
                if (livres.Numeriques[j].LivreId == id) {
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
                idTrouveNonModifie = true;
                break;
            case "Usagé":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [],
                    "Usages": [{ "LivreId": id, "Quantite": 1 }],
                    "Numeriques": []
                }))
                idTrouveNonModifie = true;
                break;
            case "Numerique":
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [],
                    "Usages": [],
                    "Numeriques": [{ "LivreId": id, "Quantite": 1 }]
                }))
                idTrouveNonModifie = true;
                break;
            default:
                localStorage.setItem('itemsPanier', JSON.stringify({
                    "Neufs": [],
                    "Usages": [],
                    "Numeriques": []
                }))
                idTrouveNonModifie = true;
        }
    }

    if (!idTrouveNonModifie) {
        switch (etatLivre) {
            case "Neuf":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Neufs.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Usagé":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Usages.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Numérique":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Numeriques.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            default:
                updateLocalStorage = false;
        }
    }
    else {
        ajouter.hidden = true;
        supprimer.hidden = false;
    }

    NbLivrePanier()
}

function supresionRapideBibli(id) {
    var supprimer = document.getElementById("suppressionRapideBibli-" + id)
    var ajouter = document.getElementById("ajoutRapide-" + id)
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))
    var idTrouveModifie = false;

    if (livres != null || livres != undefined) {

        if (livres.Neufs != null) {
            for (var j = 0; j < livres.Neufs.length; j++) {
                if (livres.Neufs[j].LivreId == id) {
                    livres.Neufs.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Usages != null) {
            for (var j = 0; j < livres.Usages.length; j++) {
                if (livres.Usages[j].LivreId == id) {
                    livres.Usages.splice(j, 1);
                    idTrouveModifie = true;
                }
            }
        }
        if (livres.Numeriques != null) {
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

    NbLivrePanier()
}
