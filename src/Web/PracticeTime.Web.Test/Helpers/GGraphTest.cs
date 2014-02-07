using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeTime.Web.Helpers;

namespace PracticeTime.Web.Test.Helpers
{
    [TestClass]
    public class GGraphTest
    {
        [TestMethod]        
        public void ConstructorTest()
        {
            GGraph graph = new GGraph();
            Assert.IsNotNull(graph);
        }

        [TestMethod]
        public void SerializeTest()
        {
            GGraph graph = new GGraph()
            {
                cols = new ColInfo[]
                {
                    new ColInfo() { id="a",label = "First",type="date"},
                    new ColInfo() { id="b",label = "Second",type="number"}
                },
                p = new Dictionary<string, string>(),
                rows = new DataPointSet[]
                {
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.ToShortDateString(),f="blah"}, 
                        new DataPoint() {v = 25}, 
                    }},
                    new DataPointSet(){ c = new DataPoint[]
                    {
                        new DataPoint() {v = DateTime.UtcNow.AddDays(-1).ToShortDateString()}, 
                        new DataPoint() {v = 25}, 
                    }},
                }
            };
            string json = new GGraphSerializer().Serailize(graph);
            Assert.IsNotNull(json);
        }
    }
}
