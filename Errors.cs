using System;
using System.Text.RegularExpressions;
using Rabbit;
using Rabbit.Localizations;

internal static class Errors
{
    /// <summary>
    /// The generate error message.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns>The <see cref="string" />.</returns>
    internal static string GenerateErrorMessage(string email, string password)
    {
        var msg = "The following errors caused authentication to fail:" + Environment.NewLine;
        if (String.IsNullOrEmpty(email))
        {
            msg = ResolveFacebookErrors(password, msg);
        }
        else
        {
            if (RabbitAuth.IsHexadecimal(password) && RabbitAuth.IsHexadecimal(email))
            {
                msg = ResolveArmorGamesErrors(email, password, msg);
            }
        }

        return msg;
    }

    private static string ResolveArmorGamesErrors(string email, string password, string msg)
    {
        msg = msg + strings.AssumeArmorGamesAuth;

        if (email.Length > 32)
        {
            msg = msg + "- " + strings.UsernameTooLong + Environment.NewLine;
        }

        if (email.Length < 32)
        {
            msg = msg + "- " + strings.UsernameTooShort + Environment.NewLine;
        }

        if (password.Length > 32)
        {
            msg = msg + "- " + strings.PasswordTooLong + Environment.NewLine;
        }

        if (password.Length < 32)
        {
            msg = msg + "- " + strings.PasswordTooShort + Environment.NewLine;
        }
        return msg;
    }

    private static string ResolveFacebookErrors(string password, string msg)
    {
        msg = msg + strings.AssumeFacebookAuth;
        if (password.Length < 100)
        {
            msg = msg + "- " + strings.TokenLessThan100Chars + Environment.NewLine;
        }

        if (!Regex.IsMatch(password, @"^[0-9a-z]$", RegexOptions.IgnoreCase))
        {
            msg = msg + "- " + strings.TokenMustBeAlphamumeric + Environment.NewLine;
        }
        return msg;
    }
}