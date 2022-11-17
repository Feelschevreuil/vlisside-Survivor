
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

function ToggleCheckPrixNeuf() {

    var input = document.querySelector("#inputPrixNeuf");
    if (input.value != 0) {
        input.disabled = false
    } else {
        document.querySelector("#PossedeNeuf").checked = false;
        input.disabled = true
    }
}
function ToggleCheckPrixNumerique() {

    var input = document.querySelector("#inputPrixNumerique");
    if (input.value != 0) {
        input.disabled = false
    } else {
        document.querySelector("#PossedeNumerique").checked = false;
        input.disabled = true
    }
}
function ToggleCheckPrixUsager() {
    var input = document.querySelector("#inputPrixUsage");
    var inputQuantiter = document.querySelector("#quantitePrixUsager");
    if (input.value != 0) {
        input.disabled = false
        inputQuantiter.disabled = false
    } else {
        document.querySelector("#PossedeUsage").checked = false;
        input.disabled = true
        inputQuantiter.disabled = true
    }
}