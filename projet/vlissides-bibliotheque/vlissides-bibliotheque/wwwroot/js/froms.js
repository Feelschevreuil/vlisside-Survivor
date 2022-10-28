function formatPhone(fakePhoneNumberInput, phoneNumberInputId) {
    if(fakePhoneNumberInput != undefined && phoneNumberInputId != undefined) {
        let phoneNumber = fakePhoneNumberInput.value;

        // remplacer tous les caractères qui ne sont pas des nombres
        phoneNumber = phoneNumber.replace(/\D/g,"");
    
        document.getElementById(phoneNumberInputId).value = parseInt(phoneNumber);
    
        let formattedPhoneNumber = phoneNumber
    
        if(phoneNumber.length < 3) {
            formattedPhoneNumber = `(${formattedPhoneNumber})`;
        }
        if(phoneNumber.length >= 3) {
            // format : (666) ***
            let tempFormattedPhoneNumber = `(${formattedPhoneNumber[0]}${formattedPhoneNumber[1]}${formattedPhoneNumber[2]}) `;
            // ajout du reste
            for(let i = 3; i < formattedPhoneNumber.length; i++) {
                tempFormattedPhoneNumber += formattedPhoneNumber;
            }
            formattedPhoneNumber = tempFormattedPhoneNumber;
        }
        if(phoneNumber.length >= 6) {
            // format : (666) 666-****
            let tempFormattedPhoneNumber = `(${formattedPhoneNumber[0]}${formattedPhoneNumber[1]}${formattedPhoneNumber[2]}) `;
                                        + `${formattedPhoneNumber[3]}${formattedPhoneNumber[4]}${formattedPhoneNumber[5]}-`;
            // ajout du reste
            for(let i = 3; i < formattedPhoneNumber.length; i++) {
                tempFormattedPhoneNumber += formattedPhoneNumber;
            }
            formattedPhoneNumber = tempFormattedPhoneNumber;
        }
    
        fakePhoneNumberInput.value =  formattedPhoneNumber;
    }
}

function formatPostalCode(postalCode) {
    
}