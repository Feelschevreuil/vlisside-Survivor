function formatPhone(fakePhoneNumberInput, phoneNumberInputId) {
    if(fakePhoneNumberInput != undefined && phoneNumberInputId != undefined) {
        let phoneNumber = fakePhoneNumberInput.value;

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
    
        // ajouter la valeur à l'input
        document.querySelector("#" + phoneNumberInputId).value = phoneNumber;
    
        // début formatage
        let formattedPhoneNumber = "";
    
        if(phoneNumber.length > 0 && phoneNumber.length < 3) {
            formattedPhoneNumber = `(${phoneNumber})`;
        } else if(phoneNumber.length >= 3 && phoneNumber.length < 6) {
            // format : (666) ***
            formattedPhoneNumber = `(${phoneNumber[0]}${phoneNumber[1]}${phoneNumber[2]}) `;
            // ajout du reste
            for(let i = 3; i < phoneNumber.length; i++) {
                formattedPhoneNumber += phoneNumber[i];
            }
        } else if(phoneNumber.length >= 6) {
            // format : (666) 666-****
            formattedPhoneNumber = `(${phoneNumber[0]}${phoneNumber[1]}${phoneNumber[2]}) `
                                + `${phoneNumber[3]}${phoneNumber[4]}${phoneNumber[5]}-`;
            // ajout du reste
            for(let i = 6; i < phoneNumber.length; i++) {
                formattedPhoneNumber += phoneNumber[i];
            }
        }
    
        fakePhoneNumberInput.value =  formattedPhoneNumber;
    }
}

function formatPostalCode(postalCode) {
    
}