initiationCards()
function initiationCards() {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))

    if (livres != null || livres != undefined) {

        if (livres.Neufs != null) {
            for (var j = 0; j < livres.Neufs.length; j++) {
                var supprimer = document.getElementById("suppressionRapideBibli-" + livres.Neufs[j].LivreId)
                var ajouter = document.getElementById("ajoutRapide-" + livres.Neufs[j].LivreId)
                if (!(ajouter == null || supprimer == null)) {
                    ajouter.hidden = true;
                    supprimer.hidden = false;
                }
            }
        }
        if (livres.Usages != null) {
            for (var j = 0; j < livres.Usages.length; j++) {
                var supprimer = document.getElementById("suppressionRapideBibli-" + livres.Usages[j].LivreId)
                var ajouter = document.getElementById("ajoutRapide-" + livres.Usages[j].LivreId)
                if (!(ajouter == null || supprimer == null)) {
                    ajouter.hidden = true;
                    supprimer.hidden = false;

                    var neuf = document.getElementById("Neuf-" + livres.Usages[j].LivreId)
                    var usage = document.getElementById("Usager-" + livres.Usages[j].LivreId)
                    var numerique = document.getElementById("Numerique-" + livres.Usages[j].LivreId)

                    if (!usage.classList.contains("bg-back")) {
                        if (neuf != null) {
                            neuf.classList.remove("bg-back")
                            neuf.classList.add("bg-top")
                            usage.classList.add("bg-back")
                            usage.classList.remove("bg-top")
                            ChangerPrix(2, livres.Usages[j].LivreId)
                        } else if (neuf == null && numerique != null) {
                            numerique.classList.remove("bg-back")
                            numerique.classList.add("bg-top")
                            usage.classList.add("bg-back")
                            usage.classList.remove("bg-top")
                            ChangerPrix(2, livres.Usages[j].LivreId)
                        }
                    }
                }
            }
        }
        if (livres.Numeriques != null) {
            for (var j = 0; j < livres.Numeriques.length; j++) {
                var supprimer = document.getElementById("suppressionRapideBibli-" + livres.Numeriques[j].LivreId)
                var ajouter = document.getElementById("ajoutRapide-" + livres.Numeriques[j].LivreId)
                if (!(ajouter == null || supprimer == null)) {
                    ajouter.hidden = true;
                    supprimer.hidden = false;

                    var neuf = document.getElementById("Neuf-" + livres.Numeriques[j].LivreId)
                    var usage = document.getElementById("Usager-" + livres.Numeriques[j].LivreId)
                    var numerique = document.getElementById("Numerique-" + livres.Numeriques[j].LivreId)

                    if (!numerique.classList.contains("bg-back")) {
                        if (neuf != null) {
                            neuf.classList.remove("bg-back")
                            neuf.classList.add("bg-top")
                            numerique.classList.add("bg-back")
                            numerique.classList.remove("bg-top")
                            ChangerPrix(1, livres.Numeriques[j].LivreId)
                        }
                    }

                }
            }
        }

    }
}