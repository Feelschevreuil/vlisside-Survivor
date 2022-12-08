
for (let img of document.querySelectorAll("#img-documentation")) {
    if (img.clientWidth < 700) {
        img.classList.add("w-50");
    } else {
        img.classList.add("w-100")
    }
}

function zoomImage(img) {
    let imageAffichageZoom = document.querySelector("#zoom-image");

    // reset la taille de l'image
    imageAffichageZoom.setAttribute("class", "");

    if (img.clientWidth < img.clientHeight) {
        imageAffichageZoom.classList.add("h-100");
    } else {
        imageAffichageZoom.classList.add("w-100")
    }

    // ajout de la source de l'image
    imageAffichageZoom.src = String(img.src);
}