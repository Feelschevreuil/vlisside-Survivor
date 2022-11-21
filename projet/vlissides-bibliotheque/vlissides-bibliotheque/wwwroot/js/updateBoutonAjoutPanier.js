initiationCards()
function initiationCards() {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))

    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                var supprimer = document.getElementById("suppressionRapideBibli-" + livres.Neuf[j])
                var ajouter = document.getElementById("ajoutRapide-" + livres.Neuf[j])
                if (!(ajouter == null || supprimer == null)) {
                    ajouter.hidden = true;
                    supprimer.hidden = false;
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                var supprimer = document.getElementById("suppressionRapideBibli-" + livres.Usage[j])
                var ajouter = document.getElementById("ajoutRapide-" + livres.Usage[j])
                if (!(ajouter == null || supprimer == null)) {
                    ajouter.hidden = true;
                    supprimer.hidden = false;
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                var supprimer = document.getElementById("suppressionRapideBibli-" + livres.Numerique[j])
                var ajouter = document.getElementById("ajoutRapide-" + livres.Numerique[j])
                if (!(ajouter == null || supprimer == null)) {
                    ajouter.hidden = true;
                    supprimer.hidden = false;
                }
            }
        }

    }
}