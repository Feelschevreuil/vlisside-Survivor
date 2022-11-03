
function getFormulaireModifierEtudiant(id) {

    fetch("/Accueil/ChangerPrix" + String(id), {
        method: 'GET',
    }).then(function (res) {
        if (!res.ok) {
            alert("L'étudiant est introuvable.")
        }

        res.text().then(function (res) {
            document.querySelector("#etudiant-" + String(id))
                .querySelector(".modal-body").innerHTML = res;
        });
    });
}

function modifierEtudiant() {
    //fetch("/Accueil/ChangerPrix" + String(id), {
    //    method: 'GET',
    //    body: data,
    //    contentType: "application/json; charset=utf-8",
    //    headers: {
    //        "Content-Type": "application/json",
    //    }
    //}).then(function (res) {
    //    if (!res.ok) {
    //        alert("L'étudiant est introuvable.")
    //    }

    //    res.text().then(function (res) {
    //        document.querySelector("#etudiant-" + String(id))
    //            .querySelector(".modal-body").innerHTML = res;
    //    });
    //});
}