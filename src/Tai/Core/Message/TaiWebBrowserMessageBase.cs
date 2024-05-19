using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Message.Types;

namespace Tai.Core.Message
{
    public class TaiWebBrowserMessageBase
    {
        public TaiWebBrowserMessageType Type { get; set; }
        public int Time { get; set; }
    }
}
