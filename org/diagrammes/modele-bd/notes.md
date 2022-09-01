
Modèle de données
=================

Propriété des objets à sauvegarder dans la base de données.

# livres

### livre bibliothèque
x ISBN
x Titre
x Résumé
x Auteur (obj)
x Programme étude (obj)
x État du livre (obj)
x Evaluation (liste objs) (seuelemnt pr complémentaires)
x Date de parutaion
x Photo (blob)

### livre étudiant
x Titre
x Programme étude
x Description
x Photo (blob)

### Évaluation
x Étoiles
x Date
x Titre
x Commentaires

### Programme d'étude
x Nom de la catégorie

### Auteur
x Nom
x Prénom

### État
x État du livre (neuf, usagé ou numérique)

### Étudiant
x Nom
x Prénom
x courriel
x Numéro de téléphone
x Programme etude (obj)
x AdresseFacturation (obj)
x AdresseLivraison (obj) [pcq, peut ê livré au travail or so on]

### Adresse
x Ville
x Numéro civique
x app
x Rue
x Code postal

# Paiements

## Facture
x Étudiant (obj)
x Liste commandes (obj)
x Type Paiement

## Informations paiement
à valider comment on link les modes de paiement avec la facture
- est-ce que l'on garde les infos du mode de paiement ou on link
  toujours au mode de paiement sur le profil?

### compte paypal
x courriel

### mode paiement
x type mode paiement

### carte crédit
x numéro de carte
x date d'expiration
x cvv

## Commande
x livre 
x type
x quantité

## Evenement
x Nom
x Début
x Fin
x Description

## Commanditaire
x Nom
x Courriel (contact)
x url (site web, optionnel)
x Message
