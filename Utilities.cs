using System.Text.RegularExpressions;

namespace Rabbit
{
    public class Utilities
    {
        /// <summary>
        ///     Check if the email is valid.
        /// </summary>
        /// <param name="strIn">The string (email).</param>
        /// <returns>Whether or not the email is valid.</returns>
        public static bool IsValidEmail(string strIn) // http://stackoverflow.com/questions/5342375/
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}