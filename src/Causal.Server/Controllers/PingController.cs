using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Causal.Model;
using Causal.Server.Database;

namespace Causal.Server.Controllers
{
    public class PingController : ApiController
    {
        private static NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public void Post(Ping ping)
        {
            ping.PingTime = DateTimeOffset.Now;

            var context = new CausalContext();
            context.Pings.Add(ping);
            context.SaveChanges();
        }
    }
}