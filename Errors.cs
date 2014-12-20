using System;
using System.Text.RegularExpressions;
using Rabbit;

static internal class Errors
{
    /// <summary>
    /// The generate error message.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns>The <see cref="string" />.</returns>
    internal static string GenerateErrorMessage(string email, string password)
    {
        var msg = String.Empty;
        if (String.IsNullOrEmpty(email))
        {
            msg = msg + strings.AssumeFacebookAuth;
            if (password.Length < 100)
            {
                msg = msg + strings.TokenLessThan100Chars;
            }

            if (!Regex.IsMatch(password, @"^[0-9a-z]$", RegexOptions.IgnoreCase))
            {
                msg = msg + strings.TokenMustBeAlphamumeric;
            }
        }
        else
        {
            if (RabbitAuth.IsHexadecimal(password) && RabbitAuth.IsHexadecimal(email))
            {
                msg = msg + strings.AssumeArmorGamesAuth;

                if (email.Length > 32)
                {
                    msg = msg + strings.UsernameTooLong;
                }

                if (email.Length < 32)
                {
                    msg = msg + strings.UsernameTooShort;
                }

                if (password.Length > 32)
                {
                    msg = msg + strings.PasswordTooLong;
                }

                if (password.Length < 32)
                {
                    msg = msg + strings.PasswordTooShort;
                }
            }

        }
            
        return msg;
    }
}