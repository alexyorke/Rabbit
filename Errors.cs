using System;
using System.Text.RegularExpressions;
using Rabbit;
using Rabbit.Localizations;

internal static class Errors
{
    /// <summary>
    /// Generates an error message.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns>The <see cref="string" />.</returns>
    internal static string GenerateErrorMessage(string email, string password)
    {
        var msg = "The following errors caused authentication to fail:" + Environment.NewLine;
        if (string.IsNullOrEmpty(email))
        {
            msg = ResolveFacebookErrors(password, msg);
        }
        else
        {
            if (Utilities.IsHexadecimal(password))
            {
                msg = Utilities.IsHexadecimal(email)
                    ? ResolveArmorGamesErrors(email, password, msg)
                    : ResolveKongregateErrors(email, password, msg);
            }
            else
            {
                // must be regular authentication
                msg = ResolveRegularErrors(email, msg);
            }
        }

        return msg;
    }

    private static string ResolveRegularErrors(string email, string msg)
    {
        if (!Utilities.IsValidEmail(email))
        {
            msg = msg + "- The email you entered was invalid.";
        }

        // I do not know of any global restrictions on passwords, unfortunately.
        return msg;
    }

    private static string ResolveKongregateErrors(string email, string password, string msg)
    {
        msg = msg + strings.AssumeKongregateAuth;

        if (!Regex.IsMatch(email, @"^\d+$"))
        {
            msg = msg + "- " + strings.KongregateUsernameMustBeInteger;
        }

        if (password.Length > 64)
        {
            msg = msg + "- " + strings.KongregatePasswordTooLong;
        }

        if (password.Length < 64)
        {
            msg = msg + "- " + strings.KongregatePasswordTooShort;
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