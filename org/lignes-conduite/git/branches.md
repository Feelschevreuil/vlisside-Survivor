
Branches
========

Comment fonctionnent les branches dans le projet?

# master (production)
Branche de production. Elle doit toujours être fonctionelle, même s'il manque des fonctionnalitées.

# develop (développement)
Branche de développement. Lorsque l'on a terminé d'implémenter une fonctionnalité ou de régler un erreur, on crée un pull request sur Azure et on assigne un membre de l'équipe qui n'a pas travaillé sur le code dans le pull request. Voir [comment approuver un pull request](code-reviews.md).

## Étapes à suivre
Lorsque vous avez terminé d'implémenter une fonctionnalité ou de corriger un erreur, veuillez suivre les étapes suivantes pour importer vos modifications sur la branche `develop`.

1. Valider que les standards ont bien été appliqués
2. Effectuer un pull request sur Azure
3. Assigner un membre de l'équipe qui n'a pas travaillé sur ce que l'on a terminé
4. Attendre que le pull request soit accepté ou rejecté
5. Si le pull request est rejeté, s'assurer de bien lire les commentaires et d'appliquer des ajustements.

# freebsd
Les branches mentionnant FreeBSD ne concernent que des modifications apportées au projet afin de permettre son exécution sur FreeBSD. Alors, voir la branche `develop-freebsd` pour plus de détails.

# Fonctionnalités
Lorsque l'on travaille sur une fonctionnalité (backlog), on crée une branche sous `feature` avec le nom de la fonctionnalité. Ajouter le paramètre `-b` si la branche n'existe pas pour la créer.

```
$ git checkout -b feature/nom-fonctionnalite develop
```

# Correction d'erreurs
Lorsque l'on travaille sur une correction d'erreur, on crée une branche sous `bug-fix` avec le nom de la correction d'erreur. Ajouter le paramètre `-b` si la branche n'existe pas pour la créer.

```
$ git checkout -b bug-fix/nom-fonctionnalite develop
```

## Pour terminer
Lorsque vous avez terminé d'implémenter une fonctionnalité, effectuez un **pull request** sur la branche `develop` pour qu'un membre de l'équipe effectue un **code review**. (Voir, effectuer un code review)
