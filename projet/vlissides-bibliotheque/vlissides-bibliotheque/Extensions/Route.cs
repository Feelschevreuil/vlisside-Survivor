namespace vlissides_bibliotheque.Extensions
{
    public static class Route
    {
        public static string Get(this HttpContext httpContext, string route)
        {
            string host = httpContext.Request.Host.ToString();
            if (host.Contains("localhost")) return $"~/{route}";
            return $"2036516/{route}";
        }
    }
}
