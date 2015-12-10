using System.Collections.Generic;
using System.ComponentModel;

namespace HopeHouse.Common.Util
{
    public static class WorkerThreadManager
    {
        #region Private Fields

        private static List<BackgroundWorker> _workerThreads;

        #endregion

        #region Public Methods

        public static void AddWorkerThread(BackgroundWorker worker)
        {
            if (_workerThreads == null)
            {
                _workerThreads = new List<BackgroundWorker>();
            }

            worker.RunWorkerCompleted += (sender, args) =>
            {
                _workerThreads.Remove(worker);
            };

            _workerThreads.Add(worker);
        }

        public static void StopAllThreads()
        {
            foreach (BackgroundWorker worker in _workerThreads)
            {
                if (!worker.CancellationPending)
                {
                    worker.CancelAsync();
                    worker.Dispose();
                }
            }
        }

        #endregion
    }
}
