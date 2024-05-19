using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Message.Types;

namespace Tai.Core.Message
{
    public class TaiSentryMessageBase
    {
        public TaiSentryMessageType Type { get; set; }
        public long CreateTime { get; set; }
    }
}
