using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class ThreadingUtilities
    {
        public static async Task UpdateTextBoxAsync(TextBox txtOutput)
        {
            for (int i = 0; i < 10; i++)
            {
                txtOutput.AppendText($"Updating line {i + 1}\n");
                await Task.Delay(1000); // Simulate a long-running task
            }
        }

       //Async/Await for Non-blocking Operations**:
       private static async Task LongRunningOperationAsync()
       {
            await Task.Run(() =>
            {
                // Simulate long operation
                Thread.Sleep(5000);
            });
       }

        // Run an action on a separate thread.
        public static void RunOnSeparateThread(Action action)
        {
            Thread newThread = new Thread(() => action());
            newThread.Start();
        }

        // Run an action asynchronously.
        public static async Task RunAsync(Action action)
        {
            await Task.Run(action);
        }

        // Delay execution for a specified amount of time.
        public static async Task DelayExecution(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        // Run multiple tasks in parallel.
        public static async Task RunTasksInParallel(params Action[] actions)
        {
            var tasks = new Task[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                tasks[i] = Task.Run(actions[i]);
            }
            await Task.WhenAll(tasks);
        }

        // Run a task with a timeout.
        public static async Task<bool> RunWithTimeout(Action action, int timeoutMilliseconds)
        {
            var task = Task.Run(action);
            if (await Task.WhenAny(task, Task.Delay(timeoutMilliseconds)) == task)
            {
                // Task completed within timeout.
                return true;
            }
            else
            {
                // Task timed out.
                return false;
            }
        }

        // Safely update UI from a separate thread.
        public static void SafeUpdateUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        // Cancel a CancellationTokenSource after a specified delay.
        public static async Task CancelAfterDelay(CancellationTokenSource cts, int delayMilliseconds)
        {
            await Task.Delay(delayMilliseconds);
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }
    }
}
