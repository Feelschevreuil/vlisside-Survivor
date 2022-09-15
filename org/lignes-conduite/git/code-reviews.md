
Code reviews
============

Lorsque vous devez effectuer un `code review`, suivez les présentes étapes pour le valider. Si le pull request ne respecte pas nos standards, indiquer où les standards ne sont pas respectés et rejeter les modifications.

## Pré-requis
- Assurez-vous de ne pas avoir travaillé sur le code que vous allez approuver.
- Avoir navigué dans le `repo` git avec Azure sous l'onglet "Pull requests".
  - Avoir choisi le pull request approprié.
  - Être dans l'onglet `commits`.

## Standards à appliquer.
Chacun des standards sont présents sous la section `org/lignes-conduite`. Vous devez être familiers avec ceux-ci pour faire des `code review`.

- Commit
- Commentaires
  - Classe
  - Méthodes/Fonctions
- Nomenclature
- La qualité de code

## Auteurs
```
CODE	AUTEUR
sm	Samuel Mongeon
sr	Sammory Roy
v	Vincent Chouinard
r	Roberto
```

## Template pour effectuer un code review
Pour chaque code review, vous pouvez utiliser ce petit template pour approuver ou désapprouver un pull request. 
> Pas absolument nécessaire, mais c'est uniquement pour s'assurer que chaque commit a bien été regardé et approuvé.

### Code review
(Si un commit n'est pas approuvé, veuillez regarder les commentaires présents sur le pull request)

Ne pas ajouter de commentaires ici, mais uniquement sur Azure où le problème est présent.

### Template
```
PASS	COMMIT		AUTEUR
[x/-]	[sum]		[auteur]
```

### Exemple
```
PASS	COMMIT		AUTEUR
x	0xf3hsef	sm
```

### Template correction de commit
Nous nous souvenons, un commit pour une seule chose. Alors, si on remarque une ou plusiuers modifications non reliées au commit, on ajoute un commentaire et on donne un exemple de ce que le commit aurait du être.

```
Devrait être dans un autre commit. (Pas une génération de base de donnée ni de migration)

Devrait être: 
\```
feat: configuration du ApplicationContext
\```
```
