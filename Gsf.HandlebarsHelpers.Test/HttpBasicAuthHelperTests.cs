using System.IO;
using HandlebarsDotNet;
using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gsf.HandlebarsHelpers.Test
{
    [TestClass]
    public class HttpBasicAuthHelperTests
    {
        [DataTestMethod]
        [DataRow("api_username", "api_password", "YXBpX3VzZXJuYW1lOmFwaV9wYXNzd29yZA==")]
        [DataRow("cats", "dogs", "Y2F0czpkb2dz")]
        [DataRow("Y2F0czpkb2dz", "YXBpX3VzZXJuYW1lOmFwaV9wYXNzd29yZA==",
            "WTJGMGN6cGtiMmR6OllYQnBYM1Z6WlhKdVlXMWxPbUZ3YVY5d1lYTnpkMjl5WkE9PQ==")]
        public void HttpBasicAuthHelperTest(string username, string password, string expectedValue)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var reader = new StreamReader(stream);
            HandlebarsHelpers.HttpBasicAuthHelper(writer, null, new []{username, password});
            writer.Flush();
            stream.Position = 0;
            var token = reader.ReadToEnd();

            Assert.IsNotNull(token);
            Assert.AreEqual(expectedValue, token);
        }

        [TestMethod]
        public void HttpBasicAuthHelperTemplateTest()
        {
            var template = "Authorization: Basic {{auth_token Username Password}}";
            var data = new
            {
                Username = "api_username",
                Password = "api_password"
            };

            Handlebars.RegisterHelper("auth_token", HandlebarsHelpers.HttpBasicAuthHelper);
            var hbTemplate = Handlebars.Compile(template);
            var final = hbTemplate(data);

            Assert.AreEqual("Authorization: Basic YXBpX3VzZXJuYW1lOmFwaV9wYXNzd29yZA==", final);
        }
    }
}