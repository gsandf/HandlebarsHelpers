using System;
using System.IO;
using System.Text;
using HandlebarsDotNet;

namespace Gsf.HandlebarsHelpers
{
    public static class GeneralHelpers
    {
        /// <summary>
        /// Helper to create HTTP Basic Auth Token
        ///
        /// Requires 2 parameters in template, username and password
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <exception cref="ArgumentException"></exception>
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
    }
}