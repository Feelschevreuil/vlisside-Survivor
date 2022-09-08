
Qualité du code
===============

Standards pour s'assurer d'avoir une bonne qualité de code uniforme à travers le projet.

Cette section porte sur
- Commentaires
- Methodes et les fonctions
- Variables

# Commentaires
**Uniquement** lorsque l'on crée une classe, une méthode ou une fonction.

Si vous devez ajouter un commentaire dans du code, créez une fonction ou une méthode! (Voir la section sur les méthodes et les fonctions)

## Template de commentaire de méthode

Voici le format que vous devez utiliser lorsque vous commentez des méthodes et des fonctions.

![Template d'un comentaire de méthode]("https://dev.azure.com/vlissides/46b56d3d-eef3-41af-b252-096b92aafb11/_apis/git/repositories/40e7d0c3-6faa-4775-9ca2-615f417d5407/items?path=/org/lignes-conduite/qualite-code/media/commentaires/template.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=documentation&resolveLfs=true&%24format=octetStream&api-version=5.0")

```
/// <summary>
/// [verbe impératif] [action] [comment].
/// </summary>
/// <param name="param0">[quoi] de [qui].</param>
/// <param name="param1">[quoi] de [qui].</param>
/// <returns>[quoi] en [type].</returns>
/// <exception cref="Exception">Dans le cas que [explication de l'erreur].</exception>
```

**Exemple**
![Exemple d'un comentaire de méthode]("https://dev.azure.com/vlissides/46b56d3d-eef3-41af-b252-096b92aafb11/_apis/git/repositories/40e7d0c3-6faa-4775-9ca2-615f417d5407/items?path=/org/lignes-conduite/qualite-code/media/commentaires/exemple.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=documentation&resolveLfs=true&%24format=octetStream&api-version=5.0")

```
/// <summary>
/// Retourne le nom et le prénom en les liant à l'aide d'une concatenation de chaînes de caractère.
/// </summary>
/// <param name="nom">Nom de la personne</param>
/// <param name="prenom">Prénom de la personne</param>
/// <returns>Le nom de la personne concatené en chaîne de caractère.</returns>
/// <exception cref="Exception">Dans le cas que le nom et le prénom sont invalides.</exception>
private string GetNomPrenom(String nom, String prenom)
```

