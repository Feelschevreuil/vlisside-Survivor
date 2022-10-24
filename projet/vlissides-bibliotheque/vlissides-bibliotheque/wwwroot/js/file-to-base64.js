
const toBase64 = file => new Promise((resolve) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
});

function convertFileToBase64(file, senderId, viewerId) {
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