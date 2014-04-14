using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Updater
{
    public partial class UpdaterService : ServiceBase
    {
        private static void Main(string[] args)
        {
            var service = new UpdaterService();

#if DEBUG
            service.Start(args);
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[] { new UpdaterService() };
            ServiceBase.Run(servicesToRun);
#endif
        }

        public UpdaterService()
        {
            InitializeComponent();
        }

        internal void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
