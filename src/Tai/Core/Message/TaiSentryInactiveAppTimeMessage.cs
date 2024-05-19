using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message
{
    public struct SentrInactiveAppTimeDataItem
    {
        public int Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TaiSentryAppInfo App { get; set; }
    }
    public class TaiSentryInactiveAppTimeMessage : TaiSentryMessageBase
    {
        public IList<SentrInactiveAppTimeDataItem> Msg { get; set; }
    }
}
