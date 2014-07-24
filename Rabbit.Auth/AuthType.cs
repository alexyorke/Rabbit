namespace Rabbit.Auth
{
    /// <summary>
    /// Enum Authentication Types
    /// </summary>
    public enum AuthType
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
        /// The Kongregate auth token
        /// </summary>
        Kongregate,
        /// <summary>
        /// The armor games auth token and auth password
        /// </summary>
        ArmorGames
    }
}