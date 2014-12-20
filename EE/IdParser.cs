// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdParser.cs" company="None">
//   Copyright 2014.
// </copyright>
// <summary>
//   The id parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rabbit
{
    /// <summary>
    /// The id parser.
    /// </summary>
    public static class IdParser
    {
        /// <summary>
        /// The parser.
        /// </summary>
        /// <param name="id">The room id.</param>
        /// <returns>The <see cref="string" />.</returns>
        /// <exception cref="System.ArgumentNullException">id;The room ID cannot be null.</exception>
        /// <exception cref="System.UriFormatException">
        /// Unknown url!
        /// or
        /// The url was correct, but the room id was invalid.
        /// </exception>
        /// <exception cref="System.FormatException">The room id url was not recognized. Make sure that the url is valid.</exception>
        /// <exception cref="FormatException">Occurs when the room id is not formatted in a proper url or is not between
        /// 9 and 14 characters or contains non-alphanumeric symbols.</exception>
        public static string Parse(string id)
        {            
            // This method is based on TakoMan02's Skylight parse url method
            // available on GitHub.
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "The room ID cannot be null.");

            if (IsValidStrictRoomId(id))
                return id;

            try
            {
                // From http://stackoverflow.com/questions/16473838/ (some parts were changed)
                var parsedUrl = new Uri(id);
                var hostParts = parsedUrl.Host.Split('.');
                var domain = string.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2).ToArray());

                if (domain != "everybodyedits.com")
                    throw new UriFormatException("Unknown url!");

                var urlId = parsedUrl.Segments.Last();

                if (IsValidStrictRoomId(urlId))
                    return urlId;

                throw new UriFormatException("The url was correct, but the room id was invalid.");
            }
            catch (UriFormatException ex)
            {
                throw new FormatException("The room id url was not recognized. Make sure that the url is valid.", ex);
            }
        }

        /// <summary>
        /// Check if the string is a valid room id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool IsValidStrictRoomId(string id)
        {
            return Regex.IsMatch(id, @"^([P|B|O]W[a-zA-Z0-9_-]+)|\$service-room\$$");
        }
    }
}
