using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConsoleApplication1.Models
{
    public class FlowScheme
    {
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        [AllowHtml]
        public string Scheme { get; set; }
    }
}
