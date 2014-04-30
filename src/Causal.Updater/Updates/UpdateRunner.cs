using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Causal.Updater.Updates
{
    class UpdateRunner
    {
        public bool BeginUpdate(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                // No update available
                return false;
            }
            else
            {
                var updater = new MsiUpdateRunner();

                var updateTask = updater.Update();
                updateTask.ContinueWith(UpdateRunner.TaskExceptionHandler, TaskContinuationOptions.OnlyOnFaulted);

                return true;
            }
        }

        private static void TaskExceptionHandler(Task updateResultTask)
        {
            try
            {
                updateResultTask.Wait();
            }
            catch (AggregateException ae)
            {
                // Log the exception
            }
        }
    }
}
