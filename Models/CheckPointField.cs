using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Models
{
    public class CheckPointField
    {
        public string type { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public string className { get; set; }
        public string subtype { get; set; }
        public string value { get; set; }
        public string asyncMethods { get; set; }
        public bool async { get; set; }
        public List<Value> values { get; set; }
    }

    public class Value
    {
        public string label { get; set; }
        public string value { get; set; }
        public bool selected { get; set; }
    }
}
