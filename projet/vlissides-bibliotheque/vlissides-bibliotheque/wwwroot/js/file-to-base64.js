
const toBase64 = file => new Promise((resolve) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
});

function convertFileToBase64(file, senderId, viewerId) {
    toBase64(file).then(function (res) {
        document.getElementById(senderId).value = res;
        document.getElementById(viewerId).src = res;
    });
}