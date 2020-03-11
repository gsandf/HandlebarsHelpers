using System;
using System.IO;
using HandlebarsDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gsf.HandlebarsHelpers.Test
{
    [TestClass]
    public class BoolHelperTests
    {
        [DataTestMethod]
        [DataRow(true, "true")]
        [DataRow(false, "false")]
        public void BoolHelperTest(object bl, string expectedValue)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var reader = new StreamReader(stream);
            
            HbHelpers.BoolHelper(writer, new object(), new [] {bl});
            writer.Flush();
            stream.Position = 0;
            var result = reader.ReadToEnd();

            Assert.AreEqual(expectedValue, result);
        }

        [DataTestMethod]
        [DataRow("yes")]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(1)]
        public void BoolHelperTest_Failure(object bl)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            
            Assert.ThrowsException<ArgumentException>(() =>
            {
                HbHelpers.BoolHelper(writer, new object(), new [] {bl});
            });
        }

        [TestMethod]
        public void BoolHelperTemplateTests()
        {

            var data = new { IsEnabled = true};

            Handlebars.RegisterHelper("bool_lower", HbHelpers.BoolHelper);
            var template =
                Handlebars.Compile("{{bool_lower IsEnabled}}");
            var json = template(data);

            Assert.AreEqual("true", json);
        }
    }
}