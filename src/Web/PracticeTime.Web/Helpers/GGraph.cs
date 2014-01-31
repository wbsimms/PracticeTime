using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace PracticeTime.Web.Helpers
{
    public class GGraph
    {
        public ColInfo[] cols { get; set; }
        public DataPointSet[] rows { get; set; }
        public Dictionary<string, string> p { get; set; }
    }

    public class ColInfo
    {
        public string id { get; set; }
        public string label { get; set; }
        public string type { get; set; }
    }

    public class DataPointSet
    {
        public DataPoint[] c { get; set; }
    }

    public class DataPoint
    {
        public object v { get; set; } // value
        public string f { get; set; } // format
    }

    public class GGraphSerializer
    {

        public string Serailize(GGraph graph)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(graph);
        }
    }
}