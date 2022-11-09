
function livreAjouteAssocie(id) {
    if(id!=undefined)
        alert(id)
}

function prixToggleNeuf() {

    var input = document.querySelector("#inputPrixNeuf");
    if (input.disabled == true) {
        input.disabled = false
    } else {
        input.disabled = true
    }
}

function prixToggleNumerique() {

    var input = document.querySelector("#inputPrixNumerique");
    if (input.disabled == true) {
        input.disabled = false
    } else {
        input.disabled = true
    }
}
function prixToggleUsager() {

    var input = document.querySelector("#inputPrixUsage");
    var inputQuantiter = document.querySelector("#quantitePrixUsager");
    if (input.disabled == true) {
        input.disabled = false
        inputQuantiter.disabled = false
    } else {
        input.disabled = true
        inputQuantiter.disabled = true
    }
}