function fillDatabase() {
    fetch(host + "TableauDeBord/RemplirBd/", {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("Une erreur c'est produite")
        }
    });
}