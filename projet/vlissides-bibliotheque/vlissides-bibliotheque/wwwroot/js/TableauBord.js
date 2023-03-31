function getFormData(formulaire) {
    let formData = new FormData(formulaire);
    let data = {};

    formData.forEach(function (value, key) {
        data[key] = value;
    });

    return data;
}

function resetMajusculeJsonKey(json) {
    let data = {};

    for (let key in json) {
        data[setPremiereLettreEnMajuscule(key)] = json[key];
    }

    return data;
}

function setPremiereLettreEnMajuscule(string) {
    return string[0].toUpperCase() + string.slice(1);
}

function possedeDesLettres(nombre) {
    if (isNaN(parseFloat(nombre)) || nombre == "") {
        return nombre = 0;
    }
    else {
        return parseFloat(nombre.replace(",", "."));
    }
}

function setInputsFormat() {
    // appel initial pour setter les inputs en cas où ils auraient déjà une valeur
    for (let phoneNumberInput of document.querySelectorAll("#phoneNumber")) {
        setPhoneNumber(phoneNumberInput.parentElement.children[1]);
    }
    for (let postalCodeInput of document.querySelectorAll("#postalCode")) {
        setPostalCode(postalCodeInput.parentElement.children[1]);
    }
}