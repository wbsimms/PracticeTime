using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.Lib.Test
{
    [TestClass]
    public class ResponseModelTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ResponseModel response = new ResponseModel();
        }

        [TestMethod]
        public void SerializeTest()
        {
            ResponseModel response = new ResponseModel()
            {
                HasErrors = false,
                HasMessages = false,
                HasNewBadges = false,
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ResponseModel));
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            serializer.Serialize(tw,response);
            Assert.IsNotNull(sb.ToString());

            StringReader sr = new StringReader(sb.ToString());
            ResponseModel model = (ResponseModel)serializer.Deserialize(sr);
            Assert.IsNotNull(model);

        }
    }
}
