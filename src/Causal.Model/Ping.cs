using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Model
{
    public class Ping
    {
        public int PingId { get; set; }

        public DateTimeOffset PingTime { get; set; }
        public Guid UpdaterId { get; set; }
        public string ComputerName { get; set; }
    }
}
