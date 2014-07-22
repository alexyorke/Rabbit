namespace Rabbit.Auth
{
    public enum AuthType
    {
        Regular,
        Facebook,
        Kongregate,
        ArmorGames,
        Unknown // used when the given auth service was incorrect for the
                // type of credentials supplied.
    }
}
