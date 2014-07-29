using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rabbit
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// The id parser.
    /// </summary>
    class IdParser
    {
        /// <summary>
        /// The parser.
        /// </summary>
        /// <param name="id">
        /// The room id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="FormatException">
        /// Occurs when the room id is not formatted in a proper url or is not between
        /// 9 and 14 characters or contains non-alphanumeric symbols.
        /// </exception>
        public string Parse(string id)
        {
            // This method is based on TakoMan02's Skylight parse url method
            // available on GitHub.
            id = id.Trim();

            if (Regex.IsMatch(id, @"^[a-zA-Z0-9_-]+$") && (id.Length <= 14) && (9 <= id.Length))
            {
                return id;
            }

            try
            {
                // From http://stackoverflow.com/questions/16473838/ (some parts were changed)
                var parsedUrl = new Uri(id);

                var hostParts = parsedUrl.Host.Split('.');
                var domain = string.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2).ToArray());

                // include a common mispellings
                if (domain != "everybodyedits.com" && domain != "everybodyedits" && domain != "everybodyedits.cm" && domain != "everybodyedits2.com")
                {
                    throw new UriFormatException();
                }

                var urlId = Convert.ToString(parsedUrl.Segments.Last());

                if (Regex.IsMatch(urlId, @"^[a-zA-Z0-9_-]+$") && (urlId.Length <= 14) && (9 <= urlId.Length))
                {
                    return urlId;
                }

                throw new UriFormatException("The url was correct, but the room id was invalid.");
            }
            catch (UriFormatException)
            {
                throw new FormatException("The room id url was not recognized. Make sure that the url is valid.");
            }
        }
    }
}
