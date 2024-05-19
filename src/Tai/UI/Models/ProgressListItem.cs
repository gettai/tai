using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tai.Core.Data.Models;

namespace Tai.UI.Models
{
    public struct ProgressListItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Progress { get; set; }
        public ImageSource Image { get; set; }
        public object Source { get; set; }
    }
}
