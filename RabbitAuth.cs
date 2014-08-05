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

namespace Rabbit
{
    using System;
    using System.Net.NetworkInformation;
    using System.Text.RegularExpressions;
    using System.Threading;

    using PlayerIOClient;

    using global::Rabbit.Auth;

    /// <summary>
    /// Authentication core.
    /// </summary>
    public class RabbitAuth
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitAuth"/> class.
        /// </summary>
        internal RabbitAuth()
        {
            UnstableNetwork = false;
            AuthenticationType = AuthenticationType.Unknown;
            CreateRoom = true;
        }

        /// <summary>
        /// The game identifier
        /// </summary>
        public const string GameId = "everybody-edits-su9rn58o40itdbnw69plyw";

        /// <summary>
        /// Gets or sets a value indicating whether the network is unstable.
        /// By default it is false. If true, multiple tries will be used to
        /// try to check if the network is available. This will increase the
        /// connection setup time.
        /// </summary>
        public static bool UnstableNetwork { get; set; }

        /// <summary>
        /// Gets or sets the authentication type.
        /// </summary>
        public static AuthenticationType AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to create a multiplayer room.
        /// </summary>
        public static bool CreateRoom { get; set; }

        /// <summary>
        /// Gets or sets the Client for the main authentication system.
        /// </summary>
        /// <value>The client.</value>
        private static Client Client { get; set; }

        /// <summary>
        /// Gets or sets the PlayerIO connection to the server.
        /// </summary>
        /// <value>The everybody edits connection.</value>
        private static Connection EeConn { get; set; }

        /// <summary>
        /// Gets the type of the authentication.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>The authentication type.</returns>
        /// <exception cref="System.InvalidOperationException">Invalid authentication type.</exception>
        public static AuthenticationType GetAuthType(string email, string password)
        {
            // ArmorGames: Both UserID and password are 32 char hexadecimal lowercase strings
            if (!string.IsNullOrEmpty(email) &&
                Regex.IsMatch(password, @"^[0-9a-f]{32}$") &&
                Regex.IsMatch(email, @"^[0-9a-f]{32}$"))
            {
                return AuthenticationType.ArmorGames;
            }

            // Kongregate: 
            // UserID is a number
            // Password is a 64 char hexadecimal lowercase string
            if (!string.IsNullOrEmpty(email) &&
                Regex.IsMatch(email, @"^\d+$") &&
                Regex.IsMatch(password, @"^[0-9a-f]{64}$"))
            {
                return AuthenticationType.Kongregate;
            }

            // Facebook: password is a 100 char alphanumerical string
            // there is no UserID supplied
            if (string.IsNullOrEmpty(email) &&
                Regex.IsMatch(password, @"^[0-9a-z]{100,}$", RegexOptions.IgnoreCase))
            {
                return AuthenticationType.Facebook;
            }

            if (!string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password))
            {
                return IsValidEmail(email) ? AuthenticationType.Regular : AuthenticationType.UserName;
            }

            // 88 character base 64 string for MouseBreaker authentication.
            // Only one token.
            if (!string.IsNullOrEmpty(email) && email.Length == 88 && !string.IsNullOrEmpty(password))
            {
                try
                {
                    Convert.FromBase64String(email);
                    return AuthenticationType.MouseBreaker;
                }
                catch (FormatException)
                {
                    // safe to ignore the exception because it is not a valid
                    // base 64 array. Keep going.
                }
            }

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("The email/token and password fields cannot be both blank.");
            }

            // Try to help the user if they entered in invalid data.
            // Guess what possible authentication type they might be trying to
            // use and tell them that there is a proper way to format it.
            throw new InvalidOperationException(GenerateErrorMessage(email, password));
        }

        /// <summary>
        /// Connects to the PlayerIO service using the provided credentials.
        /// </summary>
        /// <param name="email">
        /// Email address
        /// </param>
        /// <param name="worldId">
        /// The room id of the world to join
        /// </param>
        /// <param name="password">
        /// Password or token
        /// </param>
        /// <returns>
        /// A valid connection object.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Invalid authentication type.
        /// </exception>
        public Connection LogOn(string email, string worldId, string password = null)
        {
            if (UnstableNetwork)
            {
                var internetCheckIterations = 0;
                while (!InternetStabilityTester.IsNetworkAvailable())
                {
                    System.Diagnostics.Debug.Write(
                        "There isn't a suitable internet connection. Retrying in 2 seconds...");
                    internetCheckIterations++;
                    Thread.Sleep(2000);

                    if (internetCheckIterations > 4)
                    {
                        // Error code is ERROR_INTERNET_CANNOT_CONNECT
                        throw new NetworkInformationException(12029);
                    }
                }
            }

            // Clean the email (or token) from whitespace
            email = Regex.Replace(email, @"\s+", string.Empty);

            // Parse the world id (if it exists in another format)
            worldId = IdParser.Parse(worldId);

            // backwards compatibility
            if (AuthenticationType == AuthenticationType.Unknown)
            {
                AuthenticationType = GetAuthType(email, password);
            }

            switch (AuthenticationType)
            {
                case AuthenticationType.Facebook:
                {
                    Client = Facebook.Authenticate(password);
                    break;
                }

                case AuthenticationType.Kongregate:
                {
                    Client = Kongregate.Authenticate(email, password);
                    break;
                }

                case AuthenticationType.ArmorGames:
                {
                    Client = ArmorGames.Authenticate(email, password);
                    break;
                }

                case AuthenticationType.MouseBreaker:
                {
                    Client = MouseBreaker.Authenticate(email, password);
                    break;
                }

                case AuthenticationType.UserName:
                {
                    Client = UserName.Authenticate(email, password);
                    break;
                }

                default:
                {
                     Client = PlayerIO.QuickConnect.SimpleConnect(GameId, email, password);
                     break;
                }
            }

            if (CreateRoom)
            {
                var roomPrefix = worldId.StartsWith("BW", StringComparison.CurrentCulture) 
                    ? "Beta"
                    : "Everybodyedits";

                var serverVersion = Client.BigDB.Load("config", "config")["version"];
                EeConn = Client.Multiplayer.CreateJoinRoom(
                    worldId,
                    roomPrefix + serverVersion,
                    true,
                    null,
                    null);
            }
            else
            {
                EeConn = Client.Multiplayer.JoinRoom(
                    worldId,
                    null);
            }

            EeConn.OnMessage += this.OnMessageHandler;
            return EeConn;
        }

        /// <summary>
        /// The log in function.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <param name="worldId">
        /// The world id.
        /// </param>
        /// <returns>
        /// The <see cref="Connection"/>.
        /// </returns>
        public Connection LogOn(string token, string worldId)
        {
            return this.LogOn(token, worldId, null);
        }

        public void OnMessageHandler(object theEvent, Message message)
        {
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
                msg = msg + "Since an email, username or token was not provided, Facebook authentication " +
                    " is the only option. ";
                if (password.Length < 100)
                {
                    msg = msg + "The token should not be less than 100 characters. ";
                }

                if (password.Length > 200)
                {
                    msg = msg + "The token should not be greater than 200 characters. ";
                }

                if (!Regex.IsMatch(password, @"^[0-9a-z]$", RegexOptions.IgnoreCase))
                {
                    msg = msg + "The token should not contain non-alphanumeric characters.";
                }
            }
            else
            {
                if (Regex.IsMatch(password, @"^[0-9a-f]$") && Regex.IsMatch(email, @"^[0-9a-f]$"))
                {
                    msg = msg + "Since a token was provided for the username and password " +
                        "it was assumed that the authentication type was ArmorGames. ";
                    if (email.Length > 32)
                    {
                        msg = msg + "The username token was greater than 32 characters. ";
                    }

                    if (email.Length < 32)
                    {
                        msg = msg + "The username token was shorter than 32 characters. ";
                    }

                    if (password.Length > 32)
                    {
                        msg = msg + "The password token was greater than 32 characters.";
                    }

                    if (password.Length < 32)
                    {
                        msg = msg + "The password token was less than 32 characters.";
                    }
                }

                    msg = msg + "Since a username was provided, the regular authentication was used. ";
                    if (email.Length > 21)
                    {
                        msg = msg + "The username was longer than 20 characters.";
                    }

                    if (email.Length <= 3)
                    {
                        msg = msg + "The username was shorter than 3 characters.";
                    }
                }
            
            return msg;
        }
    }
}
