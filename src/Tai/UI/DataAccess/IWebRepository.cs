using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tai.UI.DataAccess
{
    public interface IWebRepository
    {
        IList<Core.Data.Models.Website> GetTopList(DateTime start_, DateTime end_, int num_);
    }
}
