using PlayerIOClient;

namespace Rabbit.Auth
{
    public static class Facebook
    {
        public static Client Authenticate(string token)
        {
            return PlayerIO.QuickConnect.FacebookOAuthConnect(Rabbit.GameId, token, null);
        }
    }
}