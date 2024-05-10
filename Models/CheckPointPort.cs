using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Models
{
    public class CheckPointPort
    {
        public string id { get; set; }
        public string port { get; set; }
        public string CPMethodName { get; set; }
        //public object[] CPParameters { get; set; }
        public List<CheckPointField> CPParameters { get; set; }
        public string CPMethodTitle { get; set; }
        public string CPType { get; set; }
        public string CPConfirmType { get; set; }
        public string CPSubmitter { get; set; }
        public bool CPSubmitterIsGroup { get; set; }
        public string TruePortDesc { get; set; }
        public string FalsePortDesc { get; set; }
    }
}
