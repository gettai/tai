using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message
{
    public struct TaiWebBrowserInactiveDataItem
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
    }
    public struct TaiWebBrowserInactiveData
    {
        public int Time { get; set; }
        public List<TaiWebBrowserInactiveDataItem> Urls { get; set; }
    }
    public class TaiWebBrowserInactiveMessage : TaiWebBrowserMessageBase
    {
        public TaiWebBrowserInactiveData Data { get; set; }
    }
}
