
Branches
========

Comment fonctionnent les branches dans le projet?

## Fonctionnalités
Lorsque l'on travaille sur une fonctionnalité (backlog), on crée une branche sous `feature` avec le nom de la fonctionnalité. Ajouter le paramètre `-b` si la branche n'existe pas pour la créer.

```
$ git checkout -b feature/nom-fonctionnalite develop
```

## Correction d'erreurs
Lorsque l'on travaille sur une correction d'erreur, on crée une branche sous `bug-fix` avec le nom de la correction d'erreur. Ajouter le paramètre `-b` si la branche n'existe pas pour la créer.

```
$ git checkout -b bug-fix/nom-fonctionnalite develop
```

## Pour terminer
Lorsque vous avez terminé d'implémenter une fonctionnalité, effectuez un **pull request** sur la branche `develop` pour qu'un membre de l'équipe effectue un **code review**. (Voir, effectuer un code review)
