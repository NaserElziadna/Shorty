using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Entities
{
    public class LinkStatistics : BaseEntity<int>
    {
        public long VisitsCount { get; set; }
    }
}
