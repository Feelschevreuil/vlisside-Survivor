
Projet d'intégration
====================

Équipe: vlisside

# Guidelines
Voici les règles de base à suivre pour le projet vlisside. **À lire avant de travailler sur le projet**.

## Organisaiton
Guidelines de bases sur l'organisation de l'équipe.

### Gestion du temps
Assurez-vous de toujours confirmer le temps que vous passez sur une fonctionnalité/bug fix dans le projet sur azure.

### Communications
Utiliser le groupe matrix fait pour le projet.

### Notes
Ajouter vos notes dans le répertoire de notes sur ce répertoire git.

#### Fichiers de notes
Afin de partager nos notes facilement à travers git et accessibles avec
notre éditeur de texte préféré, on les téléverse dans le repo. git.

- Nom
  - En minuscules
  - Sans accents
  - Séparé par des tirets
- Format
  - markdown

# Code
Lorsque vous travaillez sur le code du projet, suivre les règles suivantes. **À lire avant de programmer**.

## Branches
Guide sur les branches que l'on utilise dans le projet.

### master (production)
Branche de production. Elle doit toujours être fonctionnelle, même s'il
manque des fonctionnalités.

**On livre toujours un produit fonctionnel**.

### develop
On pousse les changements à chaque deux semaines (une fois le sprint
terminé.

### /feature/\<fonctionnalite>
Branche où on travaille sur les nouvelles fonctionnalités.

#### Pour commencer
Si personne ne travaille sur des fonctionnalités, cette branche peut peut 
être crée de la façon suvante:

```
$ git checkout -b feature/nom-fonctionnalite
```

#### Pour terminer
Lors que vous avez terminé d'implémenter la fonctionnalité, effectuez un
**pull request** sur la branche `develop` pour qu'un membre de l'équipe
effectue un **code review** (Voir la section de code review si vous devez
en effectuer un) afin de valider ou de rejecter le pull request.

### /bug-fix/\<bug-corriger>
Branche où on travaille sur les corrections d'erreurs.

#### Pour commencer
Si personne ne travaille sur une correction d'erreur, cette branche peut
être crée de la façon suivante:
 
```
$ git checkout -b bug-fix/nom-fonctionnalite
```

#### Pour terminer
Lors que vous avez terminé d'implémenter la correction d'erreur, effectuez
un **pull request** sur la branche `develop` pour qu'un membre de l'équipe
effectue un **code review** (Voir la section de code review si vous devez
en effectuer un) afin de valider ou de rejecter le pull request.

## Pull request
Lorsque vous effectuez un **pull request**, assurez-vous d'effectuer les étapes suivantes.

- Assurez-vous que vous avez appliqué le standard de code demandé. (Voir **Standard de code**)
- Assurez-vous que vous avez écrit vos commentaires avec un français de qualité.
- Assurez-vous d'inclure une documentation sur comment votre modification peut être testée et fournissez des données *bidon* au besoin. (Voir **Tests de fonctionnalité/bug fix**
- Assurez-vous d'avoir documenté votre nouvelle fonctionnalité. (Si applicable). (Voir **Documenter**)
- Assurez-vous d'avoir suivi le standard de commit. (Voir **Standard de commit**)

## Code review (Valider un pull request)
Lorsqu'un code review vous est assigné (Suite à un pull request), 
assurez-vous de faire les vérification suivantes:

- Confirmer que le code respecte le standard de code. (Voir **Standard de code**)
- Confirmer que les commentaires sont écris en un français de qualité.
- Confirmer qu'une documentation sur comment tester la modification est présente.
- Confirmer que la fonctionnalité est documentée (Si applicable). (Voir **Documenter**)
- Confirmer que les commits suivent le standard de commit. (Voir **Standard de commit**)
- Tester le code

Si un des points ci-haut est manquant, rejeter le pull request et expliquer pourquoi.

## Tests de fonctionnalité/bug fix
(À définir)

## Standard de commit
Utiliser le bon préfix.
Écrire un titre court.
Écrire une bonne description.

(À définir)

## Standard de code

### Brackets
(À définir)

```
public void GetFeature()
{

}
```

(À définir)

## Documenter
(À définir)

