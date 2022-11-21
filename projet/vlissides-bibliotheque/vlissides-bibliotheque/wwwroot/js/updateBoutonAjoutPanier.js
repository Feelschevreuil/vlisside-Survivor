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

                    var neuf = document.getElementById("Neuf-" + livres.Usage[j])
                    var usage = document.getElementById("Usager-" + livres.Usage[j])
                    var numerique = document.getElementById("Numerique-" + livres.Usage[j])

                    if (!usage.classList.contains("bg-back")) {
                        if (neuf != null) {
                            neuf.classList.remove("bg-back")
                            neuf.classList.add("bg-top")
                            usage.classList.add("bg-back")
                            usage.classList.remove("bg-top")
                        } else if (neuf == null && numerique != null) {
                            numerique.classList.remove("bg-back")
                            numerique.classList.add("bg-top")
                            usage.classList.add("bg-back")
                            usage.classList.remove("bg-top")
                        }
                    }
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

                    var neuf = document.getElementById("Neuf-" + livres.Numerique[j])
                    var usage = document.getElementById("Usager-" + livres.Numerique[j])
                    var numerique = document.getElementById("Numerique-" + livres.Numerique[j])

                    if (!numerique.classList.contains("bg-back")) {
                        if (neuf != null) {
                            neuf.classList.remove("bg-back")
                            neuf.classList.add("bg-top")
                            numerique.classList.add("bg-back")
                            numerique.classList.remove("bg-top")
                        }
                    }

                }
            }
        }

    }
}