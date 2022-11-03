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
    
    var fetchEnLocal = "/Accueil/ChangerPrix";
    var fetchSurServeur = "2036516/Accueil/ChangerPrix";
    var stringFech = "";
    var url = location.host;

    let text = "Mr. Blue has a blue house"
    let position = text.search("Blue");

    if (url.match("localhost") == null) {
        stringFech = fetchSurServeur;
    } else {
        stringFech = fetchEnLocal;
    }

    
    var data = JSON.stringify(DonnerRecus);


    fetch(stringFech, {
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
        var prix = data.prix
        var idRecherche = data.Id
        var balisePrix = document.querySelector('#' + "PrixLivreId" + "-" + idRecherche)
        balisePrix.innerHTML = prix + "$";
        
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