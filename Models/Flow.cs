using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Models
{
    public class Flow
    {
        public int FlowId { get; set; }
        public string FlowName { get; set; }
        public int FormId { get; set; }
        public string FlowStarter { get; set; }
        public string FlowLastSubmitter { get; set; }
        public string FlowLastPrevSubmitter { get; set; }
        public int? FlowLastStatus { get; set; }
        public string FlowLastStatusDesc { get; set; }
        public string FlowCreatedTime { get; set; }
        public string FlowLastUpdateTime { get; set; }
        public string FlowCheckPointType { get; set; }
    }
}
