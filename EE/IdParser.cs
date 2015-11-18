using System;

namespace Rabbit
{
    /// <summary>
    /// The id parser.
    /// </summary>
    public static class IdParser
    {
        /// <summary>
        /// The ID parser.
        /// </summary>
        /// <param name="id">The room id.</param>
        /// <returns>The <see cref="string" />.</returns>
        /// <exception cref="System.ArgumentNullException">The room ID cannot be null.</exception>
        /// <exception cref="System.UriFormatException">
        /// Unknown url!
        /// or
        /// The url was correct, but the room id was invalid.
        /// </exception>
        /// <exception cref="System.FormatException">The room id url was not recognized. Make sure that the url is valid.</exception>
        /// <exception cref="FormatException">Occurs when the room id is not formatted in a proper url or is not between
        /// 9 and 14 characters or contains non-alphanumeric symbols.</exception>
        public static bool TryParse(string id, out string validId)
        {
            validId = string.Empty;
            
            Uri uri;
            if (Uri.TryCreate(id, UriKind.Absolute, out uri))
            {
                if ((uri.Segments.Length == 3) && (uri.Host == "everybodyedits.com")) id = uri.Segments[2];
            }
            
            validId = id;
            return true;
        }
    }
}
