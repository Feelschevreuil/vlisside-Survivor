
Organisation git
================

Ici, vous pouvez trouver de la documentation sur comment on utilise git.

Basé sur [Best practices for using Git] ("https://deepsource.io/blog/git-best-practices/")

# Index
- Commit
- Branches

# Commit

1. Commits propres ayant une seule utilité.
2. Description de commit signifiants.
3. Utiliser le bon préfix.
4. Commitez tôt et fréquament.
5. Ne pas altérer l'historique.
6. Ne pas commiter des fichiers générés automatiquement.

## Commits propres ayant une seule utilité.

Il faut faire attention à ce que l'on inclue dans un commit. 

Par exemple, si nous travaillons sur une correction de bug, mais nous trouvons aussi un autre bug et après nous en trouvons un autre et nous corrigons tout en même temps, un effet de boule de neige se produirait dans le commit. Le commit ne serait pas clair et nous aurions beaucoup de modifications.

Ce qui serait un problème pour le code review également.

Donc, un commit doit seulement concerner une seule modification.

## Écrivez des messages de commit signifiants

Un des points les plus important d'un commit, c'est de bien décrire le commit. Ceci rend la vie plus facile à tout les membres de l'équipe ainsi qu'à la personne chargée d'effectuer le code review. 

### Prefix de commit

Inspiré de:
- [Semantic Commit Messages] ("https://sparkbox.com/foundry/semantic_commit_messages")
- [Git: Understanding Semantic Commit Messages] ("https://medium.com/@mas-al/git-understanding-semantic-commit-messages-af62d7144c51")

Afin de clarifier les commits et de bien les organiser, nous devons ajouter le bon prefix pour les commit.

```
Format: <type>(<portée>): <sujet>

feat: ajout de la suppression d'un livre
^--^  ^--------------------------------^
|     |
|     +-> Sommaire au présent
|
+-------> Type du commit: tâche, doc, feat, fix, refactor, style ou test.
```

**Définitions**
- type: un préfix court qui représente le type de changement.
- portée: une information optionnelle qui représente le contexte du changement.
- sujet: représente une description du changement actuel.

- feat: (nouvelle fonctionnalitée pour l'utilisateur, pas une fonctionnalitée pour le script de build)
- fix: (correction de bug pour l'utilisateur, pas pour le script de build)
- docs: (changements reliés à la documentation)
- style: (formatage, manque de ; et etc. Pas de changement au code de production)
- refactor: (changement de noms dans le code de production. Ex: changer le nom d'une variable)
- test: (ajout de tests manquants, refactor de tests. Pas de changement au code de production)
- build/tâche: (mise à jour des tâches récurentes. Pas de changement au code de production)

**Examples**
- tâche: Ajouter le script pour "build" avec travis
- docs: Expliquer ajouter au panier
- feat: Ajout bet dua panier
- refactor: renommer le fichier suite à 4d3d3d3
- style: conversion des tabs à des espaces
- test: s'assurer que les livres sont affichés

**Exemple de commit sans body**
```
$ git commit -m "fix(core): enlever les apis dépréciés et désuets."`
```

**Exemple do commit avec body**
```
$ git commit -m "fix(core): enlever les apis dépréciés et désuets." -m "Les apis ont été dépréciés depuis la version 8, mais ils doivent encore rester jusqu'à la version 10, mais puisqu'ils sont désuets, nous les enlevons dès que possible."
```

**Avec un éditeur de texte**
Vous pouvez aussi créer un commit avec votre éditeur de texte préféré. Faites attention à comment vous séparez le titre du body.

```
feat: ajout de la suppression d'un livre
<espace obligatoire pour séparer le titre de le body>
Body du commit.
```

**Avec un footer**
Il est possible d'ajouter un "footer" aux commits. Ceux-ci sont utilisés lors d'un gros changement, lier un commit à un problème (issue) fermé, mentionner des contributeurs et etc.

```
$ git commit -m "fix(core): enlever les apis dépréciés et désuets." \
             -m "Les apis ont été dépréciés depuis la version 8, mais ils doivent encore rester jusqu'à la version 10, mais puisqu'ils sont désuets, nous les enlevons dès que possible." \
             -m "PR fermé #495039"`
```

## Commitez tôt et fréquament

Git fonctionne bien lorsque vous commitez fréquament à la place d'attendre à rendre le commit parfait. Commiter fréquament aide à garder le code à jour avec les derniers changements pour éviter un conflit.

De plus, git garde les changements, alors ça previent la perte de données, on peut facilement annuler un changement et ça l'aide à garder une trace de ce qui a été fait avec `$ git reflog`

## Ne pas alterer l'historique

Lorsqu'un commit a été "mergé", ce n'est pas recommandé de l'alterer, puisque git crée une historique des branches.

## Ne pas commiter des fichiers générés automatiquement. 

Règle générale, on commit uniquement les fichiers qui ont eu besoin d'un effort pour les créer et qui ne peuvent pas être générés automatiquement.
Ajouter un `.gitignore` au besoin.

# Branches

## Fonctionnalités
Lorsque l'on travaille sur une fonctionnalité (backlog), on crée une branche sous `feature` avec le nom de la fonctionnalité. Ajouter le paramètre `-b` si la branche n'existe pas pour la créer.

```
$ git checkout -b feature/nom-fonctionnalite
```

## Correction d'erreurs
Lorsque l'on travaille sur une correction d'erreur, on crée une branche sous `bug-fix` avec le nom de la correction d'erreur. Ajouter le paramètre `-b` si la branche n'existe pas pour la créer.

```
$ git checkout -b bug-fix/nom-fonctionnalite
```

## Pour terminer
Lorsque vous avez terminé d'implémenter une fonctionnalité, effectuez un **pull request** sur la branche `develop` pour qu'un membre de l'équipe effectue un **code review**. (Voir, effectuer un code review)

# Pull request
(à définir)
