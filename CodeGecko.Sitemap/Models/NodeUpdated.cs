using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGecko.Packages.Umbraco.Sitemap.Models
{
    public class NodeUpdated
    {
        public int ContentId { get; set; }
        public int AverageUpdateTimespan { get; set; }
    }
}
