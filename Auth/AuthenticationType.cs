namespace Rabbit.Auth
{
    /// <summary>
    /// Authentication Types
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// The regular EE way (through the central website)
        /// </summary>
        Regular,

        /// <summary>
        /// The Facebook OAUTH authentication
        /// </summary>
        Facebook,

        /// <summary>
        /// The Kongregate authentication token
        /// </summary>
        Kongregate,

        /// <summary>
        /// The Armor Games authentication token and password
        /// </summary>
        ArmorGames,

        /// <summary>
        /// The "default" authentication type if you want Rabbit to guess which one to use.
        /// Also used to force authentication (for backwards compatibility with older
        /// systems and authentication types).
        /// </summary>
        Unknown,

        /// <summary>
        /// The invalid authentication type. Rabbit was unable to find the type of authentication used.
        /// </summary>
        Invalid,

        /// <summary>
        /// The user name authentication type. This means that the user supplied a user name
        /// which must be converted into a user id, then parsed.
        /// </summary>
        UserName,

        /// <summary>
        /// The Mousebreaker authentication.
        /// </summary>
        Mousebreaker
    }
}
