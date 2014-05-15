using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Deployment.WindowsInstaller;

namespace Causal.Updater.Updates
{
    internal sealed class MsiUpdateRunner
    {
        private static object installerMutex = new object();

        public async Task Update()
        {
            var path = @"C:\Temp\SampleProduct.msi";

            bool rebootInitiated;
            bool rebootRequired;

            var installTask = Task.Run(() =>
            {
                lock (installerMutex)
                {
                    Installer.SetInternalUI(InstallUIOptions.Silent);
                    Installer.InstallProduct(path, "");
                    rebootInitiated = Installer.RebootInitiated;
                    rebootRequired = Installer.RebootRequired;
                }
            });
            var timeoutTask = Task.Delay(TimeSpan.FromMinutes(10));

            try
            {
                // Wait for the installer to finish or the timeout to expire.
                // If the timeout expires, throw.
                if (timeoutTask == await Task.WhenAny(installTask, timeoutTask))
                {
                    throw new Exception("Update timed out");
                }
            }
            catch (InstallerException ex)
            {
                switch (ex.ErrorCode)
                {
                    case MsiExecReturnCode.Success:
                        return;

                    case MsiExecReturnCode.ProductVersion:
                        // Product is already installed
                        return;

                    default:
                        throw new Exception("MSIEXEC.EXE exited with exit code " + ex.ErrorCode);
                }
            }
        }

        private static class MsiExecReturnCode
        {
            public const int Success = 0;
            public const int SuccessRebootRequired = 3010;

            public const int InstallAlreadyRunning = 1618;

            public const int ProductVersion = 1638;
        }
    }
}
