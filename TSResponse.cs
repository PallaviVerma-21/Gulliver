using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GulliverII
{
    public class TSResponse
    {
        public string dealReference { get; set; }
        public int inserted { get; set; }
        public int updated { get; set; }
        public int failed { get; set; }
        public int deleted { get; set; }
        public List<string> errors { get; set; }
        public int packageCount { get; set; }
    }
}
