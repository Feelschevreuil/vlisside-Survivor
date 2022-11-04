namespace vlissides_bibliotheque.Extentions
{

    /// <summary>
    /// Classe <c>StringExtensions</c> contenant les extensions nécessaires pour la manipulation des chaînes de caractères.
    /// Source: https://josipmisko.com/posts/c-sharp-contains-ignore-case
    /// </summary>
    public static class StringExtensions
    {

	/// <summary>
	/// Confirme qu'une chaîne de caractères est présente dans une autre chaîne de caractères.
	/// <param name="source">Source à regarder si elle possède la chaîne de caractère cherchée.</param>
	/// <param name="substring">Chaîne de caractère à chercher.</param>
	/// </summary>
	public static bool ContainsCaseInsensitive(this string source, string substring)
	{

	    return source?.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase) > -1;
	}
    }
}
