function ChangerPrix(Etat, Id) {
  

    var DonnerRecus =
    {
        etat: Etat,
        id: Id,
       
    };
    var data = JSON.stringify(DonnerRecus);


    fetch("/Accueil/ChangerPrix", {
        method: 'Post',
        body: data,
        contentType: "application/json; charset=utf-8",
        headers: {
            "Content-Type": "application/json",
        },

    }).then(function (response) {

        return response.json();

    }).then((data) => {

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