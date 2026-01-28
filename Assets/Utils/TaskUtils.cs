using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public static class TaskUtils
    {
#if !UNITY_WEBGL
        public static Task OnNewThread(Func<Task> action, CancellationToken cancelToken)
        {
            return Task.Run(action, cancellationToken: cancelToken).LogIfFaulted();
        }

        public static Task OnNewThread(Func<Task> action)
        {
            return OnNewThread(action, CancellationToken.None);
        }
#endif

        public static Task OnSameThread(Func<Task> action)
        {
            return action?.Invoke().LogIfFaulted();
        }

        public static Task LogIfFaulted(this Task task)
        {
            return task.ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    foreach (var entry in t.Exception.InnerExceptions)
                    {
                        UnityEngine.Debug.LogError($"[TaskUtils ({Thread.CurrentThread.ManagedThreadId})] {entry.Message} :: {entry.StackTrace}");
                    }
                }
            });
        }
    }
}
