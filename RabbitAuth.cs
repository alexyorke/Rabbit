// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
// ***********************************************************************
// <copyright file="RabbitAuth.cs" company="None">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Text.RegularExpressions;
using PlayerIOClient;
using Rabbit.Auth;

namespace Rabbit
{
    /// <summary>
    /// Authentication core.
    /// </summary>
    public class RabbitAuth
    {
        // Localizable strings
        const string EmailPasswordNullError = "The email/token and password fields cannot be both blank.";
        const string AssumeFacebookAuth = "Since an email, username or token was not provided, Facebook authentication is the only option. ";
        private const string TokenLessThan100Chars = "The token should not be less than 100 characters. ";
        private const string TokenMustBeAlphamumeric = "The token should only contain alphanumeric characters.";

        private const string AssumeArmorGamesAuth = "Since a token was provided for the username and password " +
                        "it was assumed that the authentication type was Armor Games. ";

        private const string UsernameTooLong = "The username token was greater than 32 characters. ";
        private const string UsernameTooShort = "The username token was shorter than 32 characters. ";

        private const string PasswordTooLong = "The password/token was greater than 32 characters.";
        private const string PasswordTooShort = "The password/token was less than 32 characters.";
        // End localizable strings


        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitAuth"/> class.
        /// </summary>
        public RabbitAuth()
        {
            AuthenticationType = AuthenticationType.Unknown;
        }

        /// <summary>
        /// Gets or sets the authentication type.
        /// </summary>
        public AuthenticationType AuthenticationType { get; set; }

        /// <summary>
        /// Gets the type of the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authentication type.</returns>
        /// <exception cref="System.InvalidOperationException">Invalid authentication type.</exception>
        public static AuthenticationType GetAuthType(string email, string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException(EmailPasswordNullError);
            }

            // Armor Games and Kongregate require that the email field is not blank.
            if (!string.IsNullOrEmpty(email))
            {
                // Armor Games:
                // Both UserID and password are 32 char hexadecimal lowercase strings
                if (IsHexadecimal(password,32) &&
                    IsHexadecimal(email,32))
                {
                    return AuthenticationType.ArmorGames;
                }

                // Kongregate: 
                // UserID is a number
                // Password is a 64 char hexadecimal lowercase string
                if (Regex.IsMatch(email, @"^\d+$") &&
                    IsHexadecimal(password,64))
                {
                    return AuthenticationType.Kongregate;
                }
            }

            // Facebook:
            // password is a 100 char alphanumerical string
            // there is no UserID supplied
            if (string.IsNullOrEmpty(email) &&
                Regex.IsMatch(password, @"^[0-9a-z]{100,}$", RegexOptions.IgnoreCase))
            {
                return AuthenticationType.Facebook;
            }

            // Mousebreaker:
            // 88 character base 64 string for authentication.
            // Only one token.
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                if (email.Length == 88)
                {
                    try
                    {
                        Convert.FromBase64String(email);
                        return AuthenticationType.Mousebreaker;
                    }
                    catch (FormatException)
                    {
                        // safe to ignore the exception because it is not a valid
                        // base 64 array. Keep going.
                    }
                }
                // otherwise, let's hope it's regular authentication.
                return IsValidEmail(email) ? AuthenticationType.Regular : AuthenticationType.Username;

            }

            // Try to help the user if they entered in invalid data.
            // Guess what possible authentication type they might be trying to
            // use and tell them that there is a proper way to format it.
            throw new InvalidOperationException(GenerateErrorMessage(email, password));
        }

        private static bool IsHexadecimal(string password, int length = 0)
        {
            return length != 0 ? Regex.IsMatch(password, @"^[0-9a-f]{" + length + "}$") : Regex.IsMatch(password, @"^[0-9a-f]$");
        }

        /// <summary>
        /// Connects to the PlayerIO service using the provided credentials.
        /// </summary>
        /// <param name="gameId">
        /// The game id.
        /// </param>
        /// <param name="email">
        /// Email address.
        /// </param>
        /// <param name="password">
        /// Password or token.
        /// </param>
        /// <returns>
        /// A client object.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Invalid authentication type.
        /// </exception>
        public Client LogOn(string gameId, string email, string password)
        {
            // Clean the email (or token) from whitespace
            email = Regex.Replace(email, @"\s+", string.Empty);

            if (AuthenticationType == AuthenticationType.Unknown)
            {
                AuthenticationType = GetAuthType(email, password);
            }

            switch (AuthenticationType)
            {
                case AuthenticationType.Facebook:
                {
                    return Facebook.Authenticate(gameId, password);
                }

                case AuthenticationType.Kongregate:
                {
                    return Kongregate.Authenticate(gameId, email, password);
                }

                case AuthenticationType.ArmorGames:
                {
                    return ArmorGames.Authenticate(gameId, email, password);
                }

                case AuthenticationType.Mousebreaker:
                {
                    return MouseBreaker.Authenticate(gameId, email, password);
                }

                case AuthenticationType.Username:
                {
                    return Username.Authenticate(gameId, email, password);
                }

                default:
                {
                    return Simple.Authenticate(gameId, email, password);
                }
            }
        }

        /// <summary>
        /// The log in function.
        /// </summary>
        /// <param name="gameId">
        /// The game id.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Client"/>.
        /// </returns>
        public Client LogOn(string gameId, string token)
        {
            return LogOn(gameId, token, null);
        }

        /// <summary>
        /// Check if the email is valid.
        /// </summary>
        /// <param name="strIn">
        /// The string (email).
        /// </param>
        /// <returns>
        /// Whether or not the email is valid.
        /// </returns>
        private static bool IsValidEmail(string strIn) // http://stackoverflow.com/questions/5342375/
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        
        /// <summary>
        /// The generate error message.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GenerateErrorMessage(string email, string password)
        {
            var msg = string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                msg = msg + AssumeFacebookAuth;
                if (password.Length < 100)
                {
                    msg = msg + TokenLessThan100Chars;
                }

                if (!Regex.IsMatch(password, @"^[0-9a-z]$", RegexOptions.IgnoreCase))
                {
                    msg = msg + TokenMustBeAlphamumeric;
                }
            }
            else
            {
                if (IsHexadecimal(password) && IsHexadecimal(email))
                {
                    msg = msg + AssumeArmorGamesAuth;

                    if (email.Length > 32)
                    {
                        msg = msg + UsernameTooLong;
                    }

                    if (email.Length < 32)
                    {
                        msg = msg + UsernameTooShort;
                    }

                    if (password.Length > 32)
                    {
                        msg = msg + PasswordTooLong;
                    }

                    if (password.Length < 32)
                    {
                        msg = msg + PasswordTooShort;
                    }
                }

                }
            
            return msg;
        }
    }
}
