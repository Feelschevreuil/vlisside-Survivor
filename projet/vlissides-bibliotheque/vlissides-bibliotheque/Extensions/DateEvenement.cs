namespace vlissides_bibliotheque.Extensions
{
    public static class DateEvenement
    {
        public static bool CompareDate (DateTime debut, DateTime fin)
        {
            int result = DateTime.Compare(debut, fin);

            if (result < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
