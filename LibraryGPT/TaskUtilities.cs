using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class TaskUtilities
    {
        /// <summary>
        /// Executes a list of tasks in parallel and waits for all of them to complete.
        /// </summary>
        /// <param name="tasks">List of tasks to execute.</param>
        public static async Task WhenAllOrExceptionAsync(params Task[] tasks)
        {
            try
            {
                await Task.WhenAll(tasks);
            }
            catch
            {
                // Handle or log the exception if needed.
                // Rethrow the exception to be handled by the caller.
                throw;
            }
        }

        /// <summary>
        /// Executes tasks with a maximum degree of parallelism.
        /// </summary>
        /// <typeparam name="T">Type of data to process.</typeparam>
        /// <param name="data">Data to process.</param>
        /// <param name="func">Function to execute on each data item.</param>
        /// <param name="maxDegreeOfParallelism">Maximum degree of parallelism.</param>
        public static async Task ForEachAsync<T>(IEnumerable<T> data, Func<T, Task> func, int maxDegreeOfParallelism)
        {
            var throttler = new SemaphoreSlim(initialCount: maxDegreeOfParallelism);

            var tasks = data.Select(async item =>
            {
                await throttler.WaitAsync();
                try
                {
                    await func(item);
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Adds a timeout to a task.
        /// </summary>
        /// <param name="task">Task to add a timeout to.</param>
        /// <param name="timeout">Timeout duration.</param>
        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            if (task == await Task.WhenAny(task, Task.Delay(timeout)))
            {
                await task;  // Propagate any exceptions
            }
            else
            {
                throw new TimeoutException("Task exceeded the specified timeout duration.");
            }
        }

        /// <summary>
        /// Retries a task multiple times based on the specified retry count.
        /// </summary>
        /// <param name="taskFactory">Task to retry.</param>
        /// <param name="retryCount">Number of retries.</param>
        public static async Task RetryAsync(Func<Task> taskFactory, int retryCount = 3)
        {
            for (int i = 0; i < retryCount; i++)
            {
                try
                {
                    await taskFactory();
                    return;
                }
                catch
                {
                    if (i == retryCount - 1)
                    {
                        throw;  // Re-throw the exception if it's the last retry attempt
                    }
                    await Task.Delay(TimeSpan.FromSeconds(2));  // Delay before retrying
                }
            }
        }

        /// <summary>
        /// Safely executes a task and returns a default value if the task fails.
        /// </summary>
        /// <typeparam name="T">Return type of the task.</typeparam>
        /// <param name="taskFactory">Task to execute.</param>
        /// <param name="defaultValue">Default value to return if the task fails.</param>
        public static async Task<T> SafeExecuteAsync<T>(Func<Task<T>> taskFactory, T defaultValue = default)
        {
            try
            {
                return await taskFactory();
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
