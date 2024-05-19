using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Types;

namespace Tai.Core.Message
{
    public class TaiSentryStatusMessage : TaiSentryMessageBase
    {
        public UserState Msg { get; set; }
    }
}
