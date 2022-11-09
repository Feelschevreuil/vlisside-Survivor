let backspaceIsActive = false;

document.addEventListener("keydown", (event) => {
    backspaceIsActive = event.key == "Backspace";
});
document.addEventListener("keyup", (event) => {
    backspaceIsActive = event.key == "Backspace";
});

function setPhoneNumber(valueContainerInput, rawDataInputId) {
    if(valueContainerInput != undefined) {
        let phoneNumber = valueContainerInput.value;

        // supprimer un chiffre si backspace est appuyé
        if(backspaceIsActive) {
            let cursorPosition = valueContainerInput.selectionStart;
            if(phoneNumber[cursorPosition - 1] != undefined) {
                if ((cursorPosition <= 4 && phoneNumber.match(/\)/g) == undefined) || cursorPosition == 8) {
                    phoneNumber = phoneNumber.substring(0, cursorPosition - 1) + phoneNumber.substring(cursorPosition, phoneNumber.length);
                }
            }
        }
    
        // récupération de la valeur brute du no de tel.
        let phoneNumberRawData = getPhoneNumberRawData(phoneNumber);
        if(rawDataInputId != undefined) {
            document.querySelector("#" + rawDataInputId).value = phoneNumberRawData;
        }
        
        // transformation de la valeur brute du no de tel. sous le format voulu
        valueContainerInput.value = getFormattedPhoneNumber(phoneNumberRawData);
    }
}

function getPhoneNumberRawData(phoneNumber) {
    // remplacer tous les caractères qui ne sont pas des nombres
    phoneNumber = phoneNumber.replace(/\D/g,"");

    // enlever les chiffres de trop
    let phoneNumberWithMaxLength = "";
    for(let i = 0; i < phoneNumber.length; i++) {
        if(i < 10) {
            phoneNumberWithMaxLength += phoneNumber[i];
        }
    }
    phoneNumber = phoneNumberWithMaxLength;

    return phoneNumber;
}

function getFormattedPhoneNumber(phoneNumber){
    // début formatage
    let formattedPhoneNumber = "";
        
    if(phoneNumber.length > 0 && phoneNumber.length < 3) {
        formattedPhoneNumber = `(${phoneNumber})`;
    } else if(phoneNumber.length >= 3 && phoneNumber.length < 6) {
        // format : (666) ***
        formattedPhoneNumber = `(${phoneNumber[0]}${phoneNumber[1]}${phoneNumber[2]})`;
        // ajout du reste
        for(let i = 3; i < phoneNumber.length; i++) {
            formattedPhoneNumber += phoneNumber[i];
        }
    } else if(phoneNumber.length >= 6) {
        // format : (666) 666-****
        formattedPhoneNumber = `(${phoneNumber[0]}${phoneNumber[1]}${phoneNumber[2]})`
                            + `${phoneNumber[3]}${phoneNumber[4]}${phoneNumber[5]}-`;
        // ajout du reste
        for(let i = 6; i < phoneNumber.length; i++) {
            formattedPhoneNumber += phoneNumber[i];
        }
    }

    return formattedPhoneNumber;
}

function setPostalCode(valueContainerInput, rawDataInputId) {
    if(valueContainerInput != undefined) {
        let postalCode = valueContainerInput.value;

        // supprimer un caractère si backspace est appuyé
        if(backspaceIsActive) {
            let cursorPosition = valueContainerInput.selectionStart;
            if(postalCode[cursorPosition - 1] != undefined) {
                if (cursorPosition == 3) {
                    postalCode = postalCode.substring(0, cursorPosition - 1) + postalCode.substring(cursorPosition, postalCode.length);
                }
            }
        }

        // récupération de la valeur brute du code postal
        let postalCodeRawData = getPostalCodeRawData(postalCode);
        if(rawDataInputId != undefined) {
            document.querySelector("#" + rawDataInputId).value = postalCodeRawData;
        }
        
        // transformation de la valeur brute du code postal sous le format voulu
        valueContainerInput.value = getFormattedPostalCode(postalCodeRawData);
    }
}

function getPostalCodeRawData(postalCode){
    // remplacer tous les caractères qui ne sont pas des chiffres ou des lettres
    postalCode = postalCode.trim();
    postalCode = postalCode.replace(/[^a-zA-Z0-9]/g,"");

    // enlever les chiffres de trop
    let postalCodeWithMaxLength = "";
    for(let i = 0; i < postalCode.length; i++) {
        if(i < 6) {
            postalCodeWithMaxLength += postalCode[i];
        }
    }
    postalCode = postalCodeWithMaxLength;

    // forcer le code postal à être en majuscule
    postalCode = postalCode.toUpperCase();

    return postalCode;
}

function getFormattedPostalCode(postalCode) {
    // début formatage
    let formattedPostalCode = "";

    if(postalCode.length >= 3) {
        // format : A0A ***
        formattedPostalCode = `${postalCode[0]}${postalCode[1]}${postalCode[2]} `;
        // ajout du reste
        for(let i = 3; i < postalCode.length; i++) {
            formattedPostalCode += postalCode[i];
        }
    } else {
        formattedPostalCode = postalCode;
    }

    return formattedPostalCode;
}

// appel initial pour setter les inputs en cas où ils auraient déjà une valeur
for (let phoneNumberInput of document.querySelectorAll("#phoneNumber")) {
    setPhoneNumber(phoneNumberInput.parentElement.children[1]);
}
for (let postalCodeInput of document.querySelectorAll("#postalCode")) {
    setPostalCode(postalCodeInput.parentElement.children[1]);
}
