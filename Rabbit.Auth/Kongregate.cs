using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Kongregate.
    /// </summary>
    public static class Kongregate
    {
        /// <summary>
        /// Authenticates using the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Client.</returns>
        public static Client Authenticate(string email, string password)
        {
            return PlayerIO.QuickConnect.KongregateConnect(Rabbit.GameId, email, password);
        }
    }
}