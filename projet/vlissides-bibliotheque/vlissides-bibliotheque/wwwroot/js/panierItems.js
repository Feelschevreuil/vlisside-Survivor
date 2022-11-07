function getCards() {
    var div = document.getElementById("cardsIci")
    const pImpossible = document.createElement("p");
    const pVide = document.createElement("p");
    pImpossible.innerHTML = 'Désolé, votre navigateur ne supporte pas le "localStorage". Essayez de changer de navigateur si ce problème persiste.'
    pVide.innerHTML = 'Votre panier est vide!'
    var tryStorage = true
    var tryEmpty = true

    //fausse valeurs
    //var ids = ["51", "34"]
    //var ids = []
    //var objetLocalStorage = { "id": ids };
    //localStorage.setItem('itemsPanier', JSON.stringify(objetLocalStorage))

;
    try {
        typeof (Storage) !== undefined
    }
    catch(e) {
        tryStorage=false
    }

    try {
        if (JSON.parse(localStorage.getItem('itemsPanier')).id[0] == undefined)
            tryEmpty = false
    }
    catch (e) {
        tryEmpty = false
    }

    if (tryStorage) {
        if (tryEmpty) {
            alert(JSON.parse(localStorage.getItem('itemsPanier')).id[0]);
        }
        else {
            div.appendChild(pVide);
        }
    }
    else {
        div.appendChild(pImpossible);
    }
}