using PlayerIOClient;

namespace Rabbit.Auth
{
    /// <summary>
    /// Class Facebook.
    /// </summary>
    public static class Facebook
    {
        /// <summary>
        /// Authenticates the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Client.</returns>
        public static Client Authenticate(string token)
        {
            return PlayerIO.QuickConnect.FacebookOAuthConnect(Rabbit.GameId, token, null);
        }
    }
}