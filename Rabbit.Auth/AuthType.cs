// ***********************************************************************
// Assembly         : Rabbit
// Author           : Decagon
// Created          : 07-22-2014
//
// Last Modified By : Decagon
// Last Modified On : 07-24-2014
// ***********************************************************************
// <copyright file="AuthType.cs" company="">
//     Copyright 2014 (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Rabbit.Auth
{
    /// <summary>
    /// Authentication Types
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
        /// The Kongregate authentication token
        /// </summary>
        Kongregate,

        /// <summary>
        /// The armor games authentication token and password
        /// </summary>
        ArmorGames
    }
}