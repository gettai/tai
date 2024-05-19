using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Message;

namespace Tai.Core.Event
{
    public delegate void TaiSentryStatusEventHandler(object sender_, TaiSentryStatusMessage e_);
}
