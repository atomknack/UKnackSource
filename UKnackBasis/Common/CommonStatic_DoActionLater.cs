using System.Threading;
using System;
using UnityEngine;

namespace UKnack.Common;

public static partial class CommonStatic
{
    public static void DoActionLaterInNewThread(int waitForMilliseconds, Action act)
    {
        // Spawn new thread to do concurrent work
        Thread newWorkerThread = new Thread(new ThreadStart(() => WorkerToDoActionLater.DoWork(waitForMilliseconds, act)));
        newWorkerThread.Start();
    }

    private class WorkerToDoActionLater
    {
        // Method called by ThreadStart delegate to do concurrent work
        public static void DoWork(int waitForMilliseconds, Action act)
        {
            while (waitForMilliseconds > 0)
            {
                int sleep = waitForMilliseconds % 1000;
                if (sleep == 0)
                    sleep = 1000;
                waitForMilliseconds -= sleep;
                Thread.Sleep(sleep);
            }
            act();

        }
    }

}
