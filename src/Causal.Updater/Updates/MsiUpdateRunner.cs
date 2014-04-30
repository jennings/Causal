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
        public UpdateResult Update()
        {
            var path = @"C:\Temp\SampleProduct.msi";
            var args = string.Format(@"/i ""{0}"" /qn", path);
            var psi = new ProcessStartInfo()
            {
                FileName = "msiexec.exe",
                Arguments = args
            };

            var process = Process.Start(psi);
            if (process.WaitForExit(TimeSpan.FromMinutes(10).Milliseconds))
            {
                switch (process.ExitCode)
                {
                    case MsiExecReturnCode.Success:
                        return UpdateResult.Success;

                    case MsiExecReturnCode.SuccessRebootRequired:
                        return UpdateResult.RebootRequired;

                    default:
                        return UpdateResult.Failed;
                }
            }

            return UpdateResult.Failed;
        }

        private static class MsiExecReturnCode
        {
            public const int Success = 0;
            public const int SuccessRebootRequired = 3010;
        }
    }
}
