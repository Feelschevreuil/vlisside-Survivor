
# Pull request

Afin de mettre en commun notre code dans la branche `develop`, il faut effectuer un
pull request pour qu'un [code review](code-review.md) soit effectué sur le code que nous
voullons mettre en commun. En ce qui concerne un `pull request` à `master`, veuillez 
allez à la prochaine section du document.

Un `pull request` est effectué pour qu'un collègue valide que nous avons bien suivi les
lignes de conduite en ce qui concerne l'utilisation de `git` et du standard de code pour
que notre code soit organisé et uniforme.

# Faire un pull request
Pour qu'un `pull request` soit clair, on divise le pull request en trois parties.

## Description
En ce qui concerne la première partie, décrivez clairement les changements que votre
`pull request` va apporter au projet.

**Exemple**
Suite à une demande par @SAMORY ROY, le support de sqlite est enlevé, puisque personne 
à part moi (Roberto) l'utilise et que ça cause des problèmes lorsque l'on doit rouler 
la migration.

## Quoi faire pour tester la fonctionnalité ou le bug-fix
Dans cette section, décrivez comment vous pouvez tester la `fonctionnalité` ou le `bug-fix`
en étapes dans une liste en ordre.

**Exemple**
1. remplacer la connection string (du projet et du seeder)
2. "dropper" la base de données
3. rouler la migration
4. rouler le seeder
5. rouler le projet
6. valider si le projet fonctionne bien

## Status
Dans cette section, décrivez le status de votre `fonctionnalité` ou de votre `bug-fix`,
puisque des `pull request` peuvent être effectués même si la `fonctionnalité` ou le 
`bug-fix` ne sont pas terminés.

En une ligne, décrivez l'état du `pull request`. Soit 

| Projet | Modification |
|-----------|-----------:|
| Fonctionnel | Complète |

### Possibilités du status du projet
- Fonctionnel
- Partiellement fonctionnel
- Non fonctionnel

### Possibilités du status de votre fonctionnalité
- Complète
- Partiellement complète

### Erreurs connues
Dans le cas que le `pull request` est `Non fonctionnel` ou `Partiellement fonctionnel`, 
listez les erreurs et expliquez pourquoi elles arrivent.

- le seeder ne possède pas les bons id's, puisque la branche `feature/seeder` n'est 
  pas encore `mergé` dans la branche `develop`.

# Exemple d'un pull request complet

---

# Description
Suite à une demande par @SAMORY ROY, le support de sqlite est enlevé, puisque personne 
à part moi (Roberto) l'utilise et que ça cause des problèmes lorsque l'on doit rouler 
la migration.

# Quoi faire pour tester le `bug-fix`
1. remplacer la connection string (du projet et du seeder)
2. "dropper" la base de données
3. rouler la migration
4. rouler le seeder
5. rouler le projet
6. valider si le projet fonctionne bien

# Status

| Projet | Modification |
|-----------|-----------:|
| Partiellement fonctionnel | Complète |

## Erreurs connues
- le seeder ne possède pas les bons id's, puisque la branche `feature/seeder` n'est 
  pas encore `mergé` dans la branche `develop`.

---

# À master
Pour faire un `pull request` à la branche `master`, veuillez vous assurer que le projet 
est fonctionnel. Une fois fait, vous devrez créer un [tag](tag.md) de version sur azure.

(Voir [créer un tag de version](tag.md) pour savoir comment en créer un)
