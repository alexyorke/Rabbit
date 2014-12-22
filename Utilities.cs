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

        /// <summary>
        ///     Determines whether the specified password is hexadecimal.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="length">The length.</param>
        /// <returns><c>true</c> if the specified password is hexadecimal; otherwise, <c>false</c>.</returns>
        internal static bool IsHexadecimal(string password, int length = 0)
        {
            return length != 0
                ? Regex.IsMatch(password, @"^[0-9a-f]{" + length + "}$")
                : Regex.IsMatch(password, @"^[0-9a-f]");
        }
    }
}