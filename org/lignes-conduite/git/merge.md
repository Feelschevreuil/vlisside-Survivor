
# Merge

Afin d'importer les commits d'une autre branche dans une autre, veuillez faire un `merge`.

Un merge applique les commits d'une branche à la branche que vous êtes ou à une branche que
vous spécifiez.

Si vous ne spécifiez pas de branche lorsque vous exécutez la commnade, git fera un `merge`
sur la branche que vous êtes actuellement.

Alors, vérifiez dans quelle branche vous êtes pour être certains que vous n'appliquez pas
les changements dans la mauvaise branche.

```
$ git branch
```

Pour utiliser un `merge`, vous devez spécifiez la branche qui possède les commits que
vous voulez importer et la branche qui reçevra les commits (au besoin).

```
$ git merge <branche avec les commits à chercher> [branche destinataire]
```

**Exemple**
Pour importer les `commits` de la branche `develop` à la branche `feature/seeder`
```
$ git merge develop feature/seeder
```

Si vous êtes déjà dans la branche `feature/seeder`, vous pouvez omettre la mention de la
branche destinataire.

```
$ git merge develop
```
