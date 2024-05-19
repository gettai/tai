using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Message.Types;

namespace Tai.Core.Message
{
    public struct TaiSentryAppInfo
    {
        public int PID { get; set; }
        public TaiSentryAppType Type { get; set; }
        public string Process { get; set; }
        public string Description { get; set; }
        public string ExecutablePath { get; set; }
    }
}
