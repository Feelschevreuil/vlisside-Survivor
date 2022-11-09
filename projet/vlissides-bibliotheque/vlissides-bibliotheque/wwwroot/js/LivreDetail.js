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