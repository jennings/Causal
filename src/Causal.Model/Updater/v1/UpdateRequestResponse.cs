using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Model.Updater.v1
{
    public sealed class UpdateRequestResponse
    {
        public string ProductId { get; set; }
        public bool UpdateBeginning { get; set; }
    }
}
