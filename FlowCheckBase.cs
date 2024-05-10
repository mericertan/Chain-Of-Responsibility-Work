using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class FlowCheckBase
    {
        public FlowCheckBase NextCheckMethod { get; set; }

        public CheckPointPort FlowCPTestSource { get; set; }

        private EventHandler<CheckPoint> cpPassCriterionHandler;

        protected abstract void checkpointConfirm(object sender, CheckPoint criterion);

        public FlowCheckBase()
        {            
            cpPassCriterionHandler += checkpointConfirm;
        }

        public void NextCheckPoint(CheckPoint criterion)
        {            
            checkpointConfirm(this, criterion);
        }
    }
}
