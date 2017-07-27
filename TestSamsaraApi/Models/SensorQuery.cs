using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSamsaraApi.Models
{
    public class SensorQuery
    {
        public long groupId { get; set; }
        public long startMs { get; set; }
        public long endMs { get; set; }
        public long stepMs { get; set; }

        public List<series> series { get; set; }
    }
}
