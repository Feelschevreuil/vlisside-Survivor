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