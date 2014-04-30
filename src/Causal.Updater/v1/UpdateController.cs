using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Causal.Model.Updater.v1;

namespace Causal.Updater.v1
{
    public class UpdateController : ApiController
    {
        public UpdateStatus Get(string productId)
        {
            return new UpdateStatus
            {
                ProductId = productId
            };
        }

        public UpdateRequestResponse Post(UpdateRequest request)
        {
            var updater = new Causal.Updater.Updates.MsiUpdateRunner();
            updater.Update();
            return new UpdateRequestResponse
            {
                ProductId = request.ProductId,
                UpdateBeginning = true
            };
        }
    }
}
