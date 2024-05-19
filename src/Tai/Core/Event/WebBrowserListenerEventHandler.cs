using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tai.Core.Message;

namespace Tai.Core.Event
{
    public delegate void WebBrowserListenerActiveEventHandler(string browserId_, TaiWebBrowserActiveMessage msg_);
    public delegate void WebBrowserListenerInactiveEventHandler(string browserId_, TaiWebBrowserInactiveMessage msg_);
    public delegate void WebBrowserListenerClosedEventHandler(string browserId_, int id_);
    public delegate void WebBrowserListenerExitEventHandler(string browserId_);
}
