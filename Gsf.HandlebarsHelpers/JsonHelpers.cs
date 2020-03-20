using System;
using System.IO;
using HandlebarsDotNet;

namespace Gsf.HandlebarsHelpers
{
    public class JsonHelpers
    {
        /// <summary>
        /// For json templates, handles lists of items where you don't want a trailing comma
        ///
        /// Requires one parameter, the index of the current element in an #each
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
            
            writer.WriteSafeString(index > 0 ? "," : String.Empty);
        }

        /// <summary>
        /// For json templates, serializes bools lowercase rather than capital
        ///
        /// Requires one parameter, a boolean value
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void BoolHelper(TextWriter writer, dynamic context, object[] parameters)
        {
            if (parameters.Length != 1
                || parameters[0] == null
                || !Boolean.TryParse(parameters[0].ToString(), out var bl))
            {
                throw new ArgumentException("Expected one boolean parameter in BoolHelper");
            }

            writer.WriteSafeString(bl ? "true" : "false");
        }
    }
}