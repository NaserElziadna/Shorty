using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Entities
{
    public class StatisticLocation : BaseEntity<int>
    {
        public string dataDecoded { get; set; }
    }
}
