function initialisationPage() {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))

    if (livres != null || livres != undefined) {



        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neuf[j].LivreId != null) {
                    var quantiteLivre = document.querySelector("#Quantite-" + livres.Neuf[j].LivreId)
                    quantiteLivre.value = livres.Neuf[j].Quantite
                    quantiteLivre.setAttribute("max", null);
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usage[j].LivreId != null) {
                    var quantiteLivre = document.getElementById("Quantite-" + livres.Usage[j].LivreId)
                    quantiteLivre.value = livres.Usage[j].Quantite
                    quantiteLivre.setAttribute("max", 2);
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                if (livres.Numerique[j].LivreId != null) {
                    var quantiteLivre = document.getElementById("Quantite-" + livres.Numerique[j].LivreId)
                    quantiteLivre.value = livres.Numerique[j].Quantite
                    quantiteLivre.setAttribute("max", null);
                }
            }
        }
    }
}

function quantiteChange(id) {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))

    if (livres != null || livres != undefined) {

        if (livres.Neuf != null) {
            for (var j = 0; j < livres.Neuf.length; j++) {
                if (livres.Neuf[j].LivreId == id) {
                    var quantite = document.getElementById("Quantite-" + livres.Neuf[j].LivreId)
                    if (quantite.value != "") {
                        if (quantite.value > 999999) {
                            livres.Neuf[j].Quantite = 999999
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "999999"
                            updatePrix()
                        } else if (quantite.value < 1) {
                            livres.Neuf[j].Quantite = 1
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "1"
                            updatePrix()
                        } else {
                            livres.Neuf[j].Quantite = quantite.value
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            updatePrix()
                        }

                    }
                    else {
                        livres.Neuf[j].Quantite = 1
                        localStorage.clear();
                        localStorage.setItem('itemsPanier', JSON.stringify(livres));
                        quantite.value = "1"
                        updatePrix()
                    }
                }
            }
        }
        if (livres.Usage != null) {
            for (var j = 0; j < livres.Usage.length; j++) {
                if (livres.Usage[j].LivreId == id) {
                    var quantite = document.getElementById("Quantite-" + livres.Usage[j].LivreId)
                    if (quantite != null) {
                        if (quantite.value != "") {
                            if (quantite.value > 2) {
                                livres.Usage[j].Quantite = 2
                                localStorage.clear();
                                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                                quantite.value = "2"
                                updatePrix()
                            }
                            else if (quantite.value < 1) {
                                livres.Usage[j].Quantite = 1
                                localStorage.clear();
                                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                                quantite.value = "1"
                                updatePrix()
                            }
                            else {
                                livres.Usage[j].Quantite = quantite.value
                                localStorage.clear();
                                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                                updatePrix()
                            }
                        }
                        else {
                            livres.Usage[j].Quantite = 1
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "1"
                            updatePrix()
                        }
                    }
                }
            }
        }
        if (livres.Numerique != null) {
            for (var j = 0; j < livres.Numerique.length; j++) {
                if (livres.Numerique[j].LivreId == id) {
                    var quantite = document.getElementById("Quantite-" + livres.Numerique[j].LivreId)
                    if (quantite.value != "") {
                        if (quantite.value > 999999) {
                            livres.Numerique[j].Quantite = 999999
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "999999"
                            updatePrix()
                        } else if (quantite.value < 1) {
                            livres.Numerique[j].Quantite = 1
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "1"
                            updatePrix()
                        } else {
                            livres.Numerique[j].Quantite = quantite.value
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            updatePrix()
                        }

                    }
                    else {
                        livres.Numerique[j].Quantite = 1
                        localStorage.clear();
                        localStorage.setItem('itemsPanier', JSON.stringify(livres));
                        quantite.value = "1";
                        updatePrix()
                    }
                }
            }
        }
    }
}