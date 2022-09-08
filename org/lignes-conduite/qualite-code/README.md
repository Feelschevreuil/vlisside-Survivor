
Qualité du code
===============

Standards pour s'assurer d'avoir une bonne qualité de code uniforme à travers le projet.

Cette section porte sur
- Commentaires
  - Methodes et fonctions
  - Classes
- Nomenclature *(todo)*
  - Nom de variables *(todo)*
  - Nom de méthodes et fonctions *(todo)*
  - Nom des classes *(todo)*
- F.A.Q *(todo)*
  - Quand créer une méthode/fonction? *(todo)*

# Commentaires
**Uniquement** lorsque l'on crée une classe, une méthode ou une fonction.

Si vous devez ajouter un commentaire dans du code, créez une fonction ou une méthode! (Voir la section sur les méthodes et les fonctions)

## Méthode/Fonctions
Lorsque vous créez des méthodes ou des fonctions. La différence entre une méthode et une fonction est qu'une méthode retourne une variable primitive ou un objet et une fonction ne retourne rien (void).

### Template
Voici le format que vous devez utiliser lorsque vous commentez des méthodes et des fonctions.

```cs
/// <summary>
/// [verbe à indicatif présent (3ième pers. sing.)] [action] [comment].
/// </summary>
/// <param name="param0">[quoi] de [qui].</param>
/// <param name="param1">[quoi] de [qui].</param>
/// <returns>[quoi] en [type].</returns>
/// <exception cref="Exception">Dans le cas que [explication de l'erreur].</exception>
```

### Exemple

```cs
/// <summary>
/// Retourne le nom et le prénom en les liant à l'aide d'une concatenation de chaînes de caractère.
/// </summary>
/// <param name="nom">Nom de la personne</param>
/// <param name="prenom">Prénom de la personne</param>
/// <returns>Le nom de la personne concatené en chaîne de caractère.</returns>
/// <exception cref="Exception">Dans le cas que le nom et le prénom sont invalides.</exception>
private string GetNomPrenom(String nom, String prenom)
```

## Classes
Lorsque vous créez une classe, il est important d'indiquer brèvement l'utilité de la classe. Pourquoi elle existe?

```cs
/// <summary>
/// Classe <c>[Nom de la classe]</c> [Ce que fait la classe].
/// <summary>
public class Point
```

**Exemple**
```cs
/// <summary>
/// Classe <c>Point</c> modèle un point deux dimentionnel d'un avion.
/// <summary>
public class Point
```

La notation de documentation supporte d'autres balises au besoin. Par exemple, si une classe complémente une autre classe, la balise `<seealso>` peut être utilisé pour l'indiquer. 

Vous pouvez voir la liste complète des balises supportées pour documenter [ici]("https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments#d3-recommended-tags")
