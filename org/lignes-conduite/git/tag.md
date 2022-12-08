
# Tag
Lorsque du code est "mergé" à la branche `master`, vous devrez créer un tag.

Pour créer un tag:
1. Allez sur la page du projet dans Azure
2. Allez sur la barre de navigation à la gauche à l'emplacement du répertoire git
3. Choisir l'option "Tags"
4. Créer un tag en suivant le [versionnement schémantique](https://en.wikipedia.org/wiki/Software_versioning#Semantic_versioning)

## Versionnement schémantique
Voici une description de ce que chaque numéro veut dire.

```
6.6.6
| | |
| | +-- Patch
| +---- Mineure
+------ Majeure
```

### Patch
Lorsqu'un problème est résolu. Donc, lorsqu'un `bug-fix` est appliqué.

### Mineure
Lorsqu'une fonctionnalité mineure a été ajoutée. Cette fonctionnalité ne doit pas briser la
compatibilité avec les versions antécédentes. Par exemple, lorsqu'un backlog a été complété
et ajouté au projet après une version majeure.

### Majeure
Lorsqu'une fonctionnalité majeure a été ajoutée. Cette fonctionnalité brise la compatibilité
avec les versions antécédententes. Par exemple, lorsqu'un sprint avec beaucoup de nouvelles
fonctionnalités (backlogs) a été complété.
