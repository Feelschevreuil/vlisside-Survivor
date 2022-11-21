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
