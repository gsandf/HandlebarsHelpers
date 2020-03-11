using System;
using System.IO;
using System.Text;
using HandlebarsDotNet;

namespace Gsf.HandlebarsHelpers
{
    public static class HbHelpers
    {
        public static void HttpBasicAuthHelper(TextWriter writer, dynamic context, object[] parameters)
        {
            if (parameters.Length != 2 || parameters[0] == null || parameters[1] == null)
            {
                throw new ArgumentException("Must pass in username and password to HttpBasicAuthHelper");
            }

            var token = $"{parameters[0]}:{parameters[1]}";
            var tokenBytes = Encoding.ASCII.GetBytes(token);
            var tokenBase64 = Convert.ToBase64String(tokenBytes);

            writer.WriteSafeString(tokenBase64);
        }

        /// <summary>
        /// Handles lists of items where you don't want a trailing comma
        ///
        /// e.g.
        /// {{#each Item}}
        /// {{list_comma @index}}"{{this}}"
        /// {{/each}}
        ///
        /// Would only print the comma for the second and later item
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ListCommaHelper(TextWriter writer, dynamic context, object[] parameters)
        {
            if (parameters.Length != 1
                || parameters[0] == null
                || !Int32.TryParse(parameters[0].ToString(), out var index))
            {
                throw new ArgumentException("Expected one integer parameter in ListCommaHelper");
            }
            
            writer.WriteSafeString(index > 0 ? "," : string.Empty);
        }
    }
}