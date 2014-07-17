using PlayerIOClient;

namespace Rabbit.Rabbit.Auth
{
    public static class Facebook
    {
        public static Client Authenticate(string token)
        {
            return PlayerIO.QuickConnect.FacebookOAuthConnect("everybody-edits-su9rn58o40itdbnw69plyw", token, null);
        }
    }
}