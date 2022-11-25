function ChangerPrix(Etat, Id) {
  

    var DonnerRecus =
    {
        etat: Etat,
        id: Id,
       
    };
    var cercle = document.querySelector("#cercleJaune" + "N-" + DonnerRecus.id);
    var balisePrixLoading = document.querySelector('#' + "PrixLivreId" + "-" + DonnerRecus.id)
    var img = document.createElement("img");
    balisePrixLoading.innerHTML = "";
    img.src = getImage();
    img.id = "chargement";
    img.classList.add("position-absolute", "fixed-top","w-100", "h-100");
    cercle.append(img);

    var numeroEtudiant = "/" + window.location.pathname.replace(/^\/([^\/]*).*$/, '$1') + "/";
    var fetchEnLocal = "/Accueil/ChangerPrix";
    var fetchSurServeur = numeroEtudiant + "Accueil/ChangerPrix";
    var stringFetch = "";
    var url = location.host;


    if (url.match("localhost") == null) {
        stringFetch = fetchSurServeur;
    } else {
        stringFetch = fetchEnLocal;
    }
    
    var data = JSON.stringify(DonnerRecus);

    fetch(stringFetch, {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {

        return response.json();

    }).then((data) => {

        var removeImage = document.querySelector("#chargement");
        removeImage.parentElement.removeChild(removeImage);
        var prix = data.prix;
        var prixAvecPoint = parseFloat(prix.replace(",", "."));
        var prixEnDecimal = Number(prixAvecPoint.toString().match(/^\d+(?:\.\d{0,2})?/));
        if (prix.match(",") == null)
        {
            prixEnDecimal = prixEnDecimal + "." + 0 + 0
        }
        var idRecherche = data.Id;
        var balisePrix = document.querySelector('#' + "PrixLivreId" + "-" + idRecherche);
        balisePrix.innerHTML = prixEnDecimal + "$";
        
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

function getImage() {
    return "https://upload.wikimedia.org/wikipedia/commons/6/63/Elipsis.gif";
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
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Usagé":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Usage.push(valeurLivre);
                localStorage.clear();
                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                ajouter.hidden = true;
                supprimer.hidden = false;
                break;
            case "Numérique":
                var valeurLivre = { "LivreId": id, "Quantite": 1 }
                livres.Numerique.push(valeurLivre);
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

    NbLivrePanier()
}
