using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Rabbit
{
    /// <summary>
    /// Utilities such as validating an email address and some types of usernames.
    /// </summary>
    static public class Utilities
    {
        /// <summary>
        /// Check if the email is valid.
        /// </summary>
        /// <param name="strIn">The string (email).</param>
        /// <returns>
        /// Returns the email address, if it is valid. Otherwise, null.
        /// </returns>
        internal static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            try
            {
                MailAddress m = new MailAddress(strIn);
                return (strIn == m.Address);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Determines whether the specified password is hexadecimal.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="length">The length.</param>
        /// <returns><c>true</c> if the specified password is hexadecimal; otherwise, <c>false</c>.</returns>
        internal static bool IsHexadecimal(string password)
        {
            // http://stackoverflow.com/questions/223832/
            // This implementation only matches lowercase hex strings. 
            return Regex.IsMatch(password, @"\A\b[0-9a-f]+\b\Z");
        }
    }
}
