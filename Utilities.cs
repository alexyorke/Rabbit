using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Rabbit
{
    public class Utilities
    {
        /// <summary>
        /// Check if the email is valid.
        /// </summary>
        /// <param name="strIn">The string (email).</param>
        /// <returns>
        /// Returns the email address, if it is valid. Otherwise, null.
        /// </returns>
        public static bool IsValidEmail(string strIn)
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
        internal static bool IsHexadecimal(string password, int length = 0)
        {
            return length != 0
                ? Regex.IsMatch(password, @"^[0-9a-f]{" + length + "}$")
                : Regex.IsMatch(password, @"^[0-9a-f]");
        }
    }
}