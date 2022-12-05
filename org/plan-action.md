
Fonctionnalités à régler
========================

```
end	: dimanche soir
cleanup	: du lundi au mecredi AM
```
# Achat (rob)
- ~~paiement avec script~~
- webhook
- page de confirmation d'achat
- afficher l'historique des achats
- annuler un achat
- expiration de la commande après 15 minuts (background thread)
- clear le localstorage if success

# Recherche bibliothèque
- ~~simple~~
- avancée (samory/rob)

# Gestion par admin (vincent)
- lors de l'ajout d'un livre, pouvoir l'associer aux cours, aux professeur, aux maision d'édition et aux auteurs
- importation des étudiants à partir du fichier .CSV
- s'assurer que tout fonctionne
- prix pour les factures (calculer le total)

# ~~Navbar (sam)~~

## ~~Style~~

### ~~Desktop~~
- ~~Corriger la faute d'orthographe dans Bonjour~~
- ~~Bonjour NOM_ÉTUDIANT à la place du courriel~~

### ~~Mobile (sam)~~
- ~~Liens sur la nav bar et la bottom bar (S'assurer qu'ils fonctionnent)~~

# Cartes (Acceuil)
Afficher les propriétés suivantes en ordre

## Style
Ce qui doit être présent sur les cartes (en ordre)
- Boutons d'édition
- Image
- Titre
- Auteur
- Maison d'édition
- ISBN
- Date de publication
- Programme d'études
- bouton pour ajouter au panier

## Bugs
- Des fois, les cartes ne s'affichent pas bien (Lors de la recherche notamment)

# Événements
- Corriger les fautes d'orthographes
- Régler le taille des cartes (doit être uniforme)

# Recherche boutique étudiants 
- ~~implémentation~~
- ~~simple~~
- avancée

# pagination
- ~~bibliothèque~~
- ~~livres étudiants~~

---

Tests qui doivent passer
------------------------
Comme décrits par Maryse, ce qui va être testé en détails est:

> Liste de tests principaux, ceux-ci seront testés plus en détail

- recherche
  - simple
  - avancée (filtres par titres et auteurs)
- étudiant
  - boutique (usagés)
    - afficher le moyen de contact (courriel)
- achat
  - acheter des livres avec une carte de crédit
  - ajouter les livres dans le panier
- administrateur
  - gérer (ajout, suppression, modifs)
    - étudiants + importation en CSV
    - cours
    - enseignants
    - livres
    - promotions
    - commandes
