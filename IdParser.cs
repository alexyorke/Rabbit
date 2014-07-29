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

            if (Regex.IsMatch(id, "[htp:/w.evrybodis.comga]{0,36}[a-zA-Z0-9_-]{13}"))
            {
                try
                {
                    var parsedUrl = new Uri(id);
                    var finalUrl = Convert.ToString(parsedUrl.Segments.Last());

                    return finalUrl;
                }
                catch (UriFormatException)
                {
                    throw new FormatException("The room id format was not recognized.");
                }
            }

            return null;
        }
    }
}
