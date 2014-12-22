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
using Rabbit.Localizations;

namespace Rabbit
{
    /// <summary>
    ///     Authentication core.
    /// </summary>
    public class RabbitAuth
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RabbitAuth" /> class.
        /// </summary>
        public RabbitAuth()
        {
            AuthenticationType = AuthenticationType.Unknown;
        }

        /// <summary>
        ///     Gets or sets the authentication type.
        /// </summary>
        /// <value>The type of the authentication.</value>
        public AuthenticationType AuthenticationType { get; set; }

        /// <summary>
        ///     Gets the type of the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authentication type.</returns>
        /// <exception cref="System.InvalidOperationException">Invalid authentication type.</exception>
        public static AuthenticationType GetAuthType(string email, string password)
        {
            // Armor Games and Kongregate require that the email field is not blank.
            if (!string.IsNullOrEmpty(email))
            {
                // Armor Games and Kongregate require that the password is hexadecimal

                if (Utilities.IsHexadecimal(password))
                {
                    // Armor Games:
                    // Username: a 32 character lowercase hexadecimal string
                    // Password: a 32 character lowercase hexadecimal string
                    if (password.Length == 32 && Utilities.IsHexadecimal(email, 32))
                    {
                        return AuthenticationType.ArmorGames;
                    }

                    // Kongregate: 
                    // Username: a positive integer
                    // Password: a 64 character lowercase hexadecimal string
                    if (Regex.IsMatch(email, @"^\d+$") &&
                        password.Length == 64)
                    {
                        return AuthenticationType.Kongregate;
                    }
                }
                // Mousebreaker:
                // Username: a valid email address
                // Password: 88 character base 64 string
                if (string.IsNullOrEmpty(password))
                    throw new InvalidOperationException(Errors.GenerateErrorMessage(email, password));
                if (password.Length != 88)
                    return Utilities.IsValidEmail(email) ? AuthenticationType.Regular : AuthenticationType.Username;
                try
                {
                    Convert.FromBase64String(password);
                    return AuthenticationType.Mousebreaker;
                }
                catch (FormatException)
                {
                    // safe to ignore the exception because it is not a valid
                    // base 64 array.
                }
                // otherwise, let's hope it's regular authentication.
                return Utilities.IsValidEmail(email) ? AuthenticationType.Regular : AuthenticationType.Username;
            }

            // the email and password cannot both be blank
            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException(strings.EmailPasswordNullError);
            }

            // Facebook:
            // Username: N/A
            // Password: 100 character (or greater) alphanumerical string
            if (Regex.IsMatch(password, @"^[0-9a-z]{100,}$", RegexOptions.IgnoreCase))
            {
                return AuthenticationType.Facebook;
            }


            // Try to help the user if they entered in invalid data.
            // Guess what possible authentication type they might be trying to
            // use and tell them that there is a proper way to format it.
            throw new InvalidOperationException(Errors.GenerateErrorMessage(email, password));
        }

        /// <summary>
        ///     Connects to the PlayerIO service using the provided credentials.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="email">Email address.</param>
        /// <param name="password">Password or token.</param>
        /// <param name="createRoom">if set to <c>true</c> secure API requests will be used.</param>
        /// <returns>A client object.</returns>
        /// <exception cref="System.InvalidOperationException">Invalid authentication type.</exception>
        public Client LogOn(string gameId, string email, string password, bool createRoom = true)
        {
            PlayerIO.UseSecureApiRequests = createRoom;

            // Clean the email (or token), and gameId from whitespace
            email = Regex.Replace(email, @"\s+", string.Empty);
            gameId = Regex.Replace(gameId, @"\s+", string.Empty);


            if (!Regex.IsMatch(gameId, @"^[0-9a-zA-Z\-]+$"))
            {
                throw new ArgumentException(strings.RabbitAuth_LogOn_The_game_ID_contains_an_invalid_character_,
                    "gameId");
            }


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
        ///     The log in function.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="token">The token.</param>
        /// <param name="shouldUseSecureApiRequests">if set to <c>true</c> secure API requests will be used.</param>
        /// <returns>
        ///     The <see cref="Client" />.
        /// </returns>
        public Client LogOn(string gameId, string token, bool shouldUseSecureApiRequests = false)
        {
            return LogOn(gameId, token, null, shouldUseSecureApiRequests);
        }
    }
}