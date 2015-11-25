using System;
using System.Text.RegularExpressions;
using PlayerIOClient;
using Rabbit.Auth;
using System.Security.Authentication;

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
            // All methods of authentication require that the username and password fields are not empty
            if (string.IsNullOrEmpty(password))
            {
                throw new AuthenticationException();
            }

            if (string.IsNullOrEmpty(email))
            {
                // Facebook:
                // Username: N/A
                // Password: 90 character (or greater) alphanumerical string
                if (Regex.IsMatch(password, @"[0-9a-z]$", RegexOptions.IgnoreCase) && password.Length > 90)
                {
                    return AuthenticationType.Facebook;
                }

                return AuthenticationType.Invalid;
            }

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
                if (Regex.IsMatch(email, @"^\d+$") && password.Length == 64)
                {
                    return AuthenticationType.Kongregate;
                }
            }

            // Mousebreaker:
            // Username: a valid email address
            // Password: 88 character base 64 string
            if (password.Length == 88)
            {
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
            }

            return Utilities.IsValidEmail(email) ? AuthenticationType.Regular : AuthenticationType.UserName;
        }

        /// <summary>
        ///     Connects to the PlayerIO service using the provided credentials.
        /// </summary>
        /// <param name="gameId">The game id.</param>
        /// <param name="email">Email address.</param>
        /// <param name="password">Password or token.</param>
        /// <returns>A client object.</returns>
        /// <exception cref="System.InvalidOperationException">Invalid authentication type.</exception>
        internal Client LogOn(string gameId, string email, string password)
        {
            // Clean the email (or token), and gameId from whitespace
            email = Regex.Replace(email, @"\s+", string.Empty);
            gameId = Regex.Replace(gameId, @"\s+", string.Empty);

            if (AuthenticationType == AuthenticationType.Unknown)
                AuthenticationType = GetAuthType(email, password);

            switch (AuthenticationType)
            {
                case AuthenticationType.Facebook:     return Facebook.Authenticate(gameId, password);
                case AuthenticationType.Kongregate:   return Kongregate.Authenticate(gameId, email, password);
                case AuthenticationType.ArmorGames:   return ArmorGames.Authenticate(gameId, email, password);
                case AuthenticationType.Mousebreaker: return MouseBreaker.Authenticate(gameId, email, password);
                case AuthenticationType.UserName:     return UserName.Authenticate(gameId, email, password);
                default:                              return Simple.Authenticate(gameId, email, password);
            }
        }
    }
}
