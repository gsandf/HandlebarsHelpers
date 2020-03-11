using System;
using System.Collections.Generic;
using System.IO;
using HandlebarsDotNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gsf.HandlebarsHelpers.Test
{
    [TestClass]
    public class ListCommaHelperTests
    {

        [DataTestMethod]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(5, true)]
        [DataRow("0", false)]
        [DataRow("1", true)]
        public void ListCommaHelperTest(object index, bool hasComma)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var reader = new StreamReader(stream);
            HbHelpers.ListCommaHelper(writer, new object(), new object[] {index});
            writer.Flush();
            stream.Position = 0;
            var comma = reader.ReadToEnd();

            Assert.IsNotNull(comma);
            Assert.AreEqual(hasComma, "," == comma);
        }

        [DataTestMethod]
        [DataRow("one")]
        [DataRow(null)]
        [DataRow("")]
        public void ListCommaHelperTests_Failure(object index)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            
            Assert.ThrowsException<ArgumentException>(() =>
            {
                HbHelpers.ListCommaHelper(writer, new object(), new object[] {index});
            });
        }

        [TestMethod]
        public void ListCommaHelperTestsWithData()
        {
            var data = new
            {
                Items = new List<string> {"one", "two", "three", "four", "five"}
            };

            Handlebars.RegisterHelper("list_comma", HbHelpers.ListCommaHelper);
            var template =
                Handlebars.Compile("{\"Items\":[{{#each Items}}{{list_comma @index}}\"{{this}}\"{{/each}}]}");
            var json = template(data);

            Assert.AreEqual("{\"Items\":[\"one\",\"two\",\"three\",\"four\",\"five\"]}", json);
        }
        
    }
}