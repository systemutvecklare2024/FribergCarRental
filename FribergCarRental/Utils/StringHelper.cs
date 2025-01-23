namespace FribergCarRental.Utils
{
    public static class StringHelper
    {
        public static string Capitalize(string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
