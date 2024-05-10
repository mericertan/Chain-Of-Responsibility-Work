using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsoleApplication1.Models
{
    public class CheckPoint
    {
        public int FlowId { get; set; }
        public int FormId { get; set; }
        public string CheckpointTemplate { get; set; }
        public string CheckpointValues { get; set; }
        [AllowHtml]
        public string CheckpointScheme { get; set; }
        public string CheckpointSource { get; set; }
        public string CheckpointTarget { get; set; }
        public string CheckpointType { get; set; }
        public string CheckpointSubmitter { get; set; }
        public string CheckpointPrevSubmitter { get; set; }
        public int? Status { get; set; }
        public string StatusDesc { get; set; }
        public string LastUpdateTime { get; set; }
    }

    public class CheckPointResult
    {
        public bool choice { get; set; }
        public string submitter { get; set; }
        public string desc { get; set; }
    }
}
