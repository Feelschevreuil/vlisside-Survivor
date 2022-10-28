window.addEventListener("resize", () => {
    setBottomNavbar();
});

function setBottomNavbar() {
    let screenWidth = window.innerWidth;
    let bottomNav = document.querySelector("#bottomNav");
    let copyrightInfo = bottomNav.querySelector("#copyrightInfo");
    let bottomNavContent = document.querySelector("#bottomNavContent");
    let livreLinks = document.querySelectorAll("#livreLink");

    if (screenWidth <= 575) {

        bottomNav.classList.replace("position-absolute", "position-fixed");
        copyrightInfo.classList.replace("d-block", "d-none");
        bottomNavContent.classList.replace("d-none", "d-block");
        for (let livreLink of livreLinks) {
            livreLink.classList.add("d-none");
        }
    } else {

        bottomNav.classList.replace("position-fixed", "position-absolute");
        copyrightInfo.classList.replace("d-none", "d-block");
        bottomNavContent.classList.replace("d-block", "d-none");
        for (let livreLink of livreLinks) {
            livreLink.classList.remove("d-none");
        }
    }
}

setBottomNavbar();