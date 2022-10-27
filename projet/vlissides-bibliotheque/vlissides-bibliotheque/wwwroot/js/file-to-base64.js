/// <summary>
/// Convertit un fichier en base 64.
/// </summary>
/// <returns>Object Promise avec fichier en base 64</returns>
function toBase64(file) {
    return new Promise((resolve) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
    });
}

/// <summary>
/// Récupère un fichier en base 64 et l'affiche.
/// </summary>
/// <returns></returns>
function getAndShowBase64(file, senderId, viewerId) {
    if (file != undefined && typeof file == "object") {
        toBase64(file).then(function (res) {
            if (senderId != undefined) {
                document.getElementById(senderId).value = res;
            }
            if (viewerId != undefined) {
                document.getElementById(viewerId).src = res;
            }
        });
    }
}