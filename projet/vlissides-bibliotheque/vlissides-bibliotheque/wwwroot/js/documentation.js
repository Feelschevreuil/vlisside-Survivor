
for (let img of document.querySelectorAll("#img-documentation")) {
    if (img.clientWidth < 700) {
        img.classList.add("w-50");
    } else {
        img.classList.add("w-100")
    }
}