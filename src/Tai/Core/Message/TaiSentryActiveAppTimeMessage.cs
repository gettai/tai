using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.Core.Message
{
    public struct Handle
    {
        public int Value { get; set; }
    }
    public struct Window
    {
        public string ClassName { get; set; }
        public string Title { get; set; }
        public Handle Handle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    public struct SentrActiveAppTimeData
    {
        public int Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TaiSentryAppInfo App { get; set; }
        public Window Window { get; set; }

    }
    public class TaiSentryActiveAppTimeMessage : TaiSentryMessageBase
    {
        public SentrActiveAppTimeData Msg { get; set; }
    }
}
