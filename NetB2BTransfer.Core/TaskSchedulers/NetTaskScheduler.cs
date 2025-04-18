using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetB2BTransfer.Core.TaskSchedulers
{
    public class NetTaskScheduler : TaskScheduler, IDisposable
    {
        private BlockingCollection<Task> tasksCollection = new BlockingCollection<Task>();
        private readonly Thread mainThread = null;

        public NetTaskScheduler()
        {
            mainThread = new Thread(new ThreadStart(Execute));
            if (!mainThread.IsAlive)
            {
                mainThread.Start();
            }
        }
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasksCollection.ToArray();
        }

        protected override void QueueTask(Task task)
        {
            if (task != null)
            {
                tasksCollection.Add(task);
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        private void Execute()
        {
            foreach (var task in tasksCollection.GetConsumingEnumerable())
            {
                TryExecuteTask(task);
            }
        }

        public void Dispose(bool disposing)
        {
            if (!disposing) return;
            tasksCollection.CompleteAdding();
            tasksCollection.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
