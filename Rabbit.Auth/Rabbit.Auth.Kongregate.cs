using PlayerIOClient;

namespace Rabbit
{
    public static class Kongregate
    {
        public static Client Authenticate(string email, string password)
        {
            return PlayerIO.QuickConnect.KongregateConnect("everybody-edits-su9rn58o40itdbnw69plyw", email, password);
        }
    }
}