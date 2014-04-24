using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Causal.Updater
{
    internal sealed class UpdaterService : ServiceBase
    {
        private IDisposable webApp;

        static void Main()
        {
            var service = new UpdaterService();
#if DEBUG
            service.OnStart(new string[0]);
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase.Run(service);
#endif
        }

        protected override void OnStart(string[] args)
        {
            var baseAddress = "http://+:32111/causalupdater/";
            this.webApp = WebApp.Start<Startup>(url: baseAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.webApp != null)
                    this.webApp.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
