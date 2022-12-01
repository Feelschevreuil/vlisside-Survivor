function quantiteChange(id) {
    var livres = JSON.parse(localStorage.getItem('itemsPanier'))

    if (livres != null || livres != undefined) {

        if (livres.Neufs != null) {
            for (var j = 0; j < livres.Neufs.length; j++) {
                if (livres.Neufs[j].LivreId == id) {
                    var quantite = document.getElementById("Quantite-" + livres.Neufs[j].LivreId)
                    if (quantite.value != "") {
                        if (quantite.value > 999999) {
                            livres.Neufs[j].Quantite = 999999
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "999999"
                            updatePrix()
                            NbLivrePanier()
                        } else if (quantite.value < 1) {
                            livres.Neufs[j].Quantite = 1
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "1"
                            updatePrix()
                            NbLivrePanier()
                        } else {
                            livres.Neufs[j].Quantite = quantite.value
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            updatePrix()
                            NbLivrePanier()
                        }

                    }
                    else {
                        livres.Neufs[j].Quantite = 1
                        localStorage.clear();
                        localStorage.setItem('itemsPanier', JSON.stringify(livres));
                        quantite.value = "1"
                        updatePrix()
                        NbLivrePanier()
                    }
                }
            }
        }
        if (livres.Usages != null) {
            for (var j = 0; j < livres.Usages.length; j++) {
                if (livres.Usages[j].LivreId == id) {
                    var quantite = document.getElementById("Quantite-" + livres.Usages[j].LivreId)
                    if (quantite != null) {
                        if (quantite.value != "") {
                            if (quantite.value > 2) {
                                livres.Usages[j].Quantite = 2
                                localStorage.clear();
                                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                                quantite.value = "2"
                                updatePrix()
                                NbLivrePanier()
                            }
                            else if (quantite.value < 1) {
                                livres.Usages[j].Quantite = 1
                                localStorage.clear();
                                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                                quantite.value = "1"
                                updatePrix()
                                NbLivrePanier()
                            }
                            else {
                                livres.Usages[j].Quantite = quantite.value
                                localStorage.clear();
                                localStorage.setItem('itemsPanier', JSON.stringify(livres));
                                updatePrix()
                                NbLivrePanier()
                            }
                        }
                        else {
                            livres.Usages[j].Quantite = 1
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "1"
                            updatePrix()
                            NbLivrePanier()
                        }
                    }
                }
            }
        }
        if (livres.Numeriques != null) {
            for (var j = 0; j < livres.Numeriques.length; j++) {
                if (livres.Numeriques[j].LivreId == id) {
                    var quantite = document.getElementById("Quantite-" + livres.Numeriques[j].LivreId)
                    if (quantite.value != "") {
                        if (quantite.value > 999999) {
                            livres.Numeriques[j].Quantite = 999999
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "999999"
                            updatePrix()
                            NbLivrePanier()
                        } else if (quantite.value < 1) {
                            livres.Numeriques[j].Quantite = 1
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            quantite.value = "1"
                            updatePrix()
                            NbLivrePanier()
                        } else {
                            livres.Numeriques[j].Quantite = quantite.value
                            localStorage.clear();
                            localStorage.setItem('itemsPanier', JSON.stringify(livres));
                            updatePrix()
                            NbLivrePanier()
                        }

                    }
                    else {
                        livres.Numeriques[j].Quantite = 1
                        localStorage.clear();
                        localStorage.setItem('itemsPanier', JSON.stringify(livres));
                        quantite.value = "1";
                        updatePrix()
                        NbLivrePanier()
                    }
                }
            }
        }
    }
}