using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message
{
   
    public class TaiWebBrowserClosedMessage : TaiWebBrowserMessageBase
    {
        public int Data { get; set; }
    }
}
