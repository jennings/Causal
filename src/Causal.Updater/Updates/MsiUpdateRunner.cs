using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Updater.Updates
{
    internal sealed class MsiUpdateRunner
    {
        public async Task Update()
        {
            var path = @"C:\Temp\SampleProduct.msi";
            var args = string.Format(@"/i ""{0}"" /qn", path);
            var psi = new ProcessStartInfo()
            {
                FileName = "msiexec.exe",
                Arguments = args
            };

            var process = Process.Start(psi);
            var installTask = Task.Run(() => process.WaitForExit());
            var timeoutTask = Task.Delay(TimeSpan.FromMinutes(10));

            var completedTask = await Task.WhenAny(installTask, timeoutTask);

            if (completedTask == timeoutTask)
            {
                throw new Exception("Update timed out");
            }
            else if (!process.HasExited)
            {
                throw new Exception("Update timeout did not fire, but the process has not exited");
            }

            switch (process.ExitCode)
            {
                case MsiExecReturnCode.Success:
                    return;

                case MsiExecReturnCode.ProductVersion:
                    // Product is already installed
                    return;

                case MsiExecReturnCode.SuccessRebootRequired:
                    throw new Exception("Reboot required");

                default:
                    throw new Exception("MSIEXEC.EXE exited with exit code " + process.ExitCode);
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
