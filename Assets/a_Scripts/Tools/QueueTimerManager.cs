using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HollowKnight.Tools
{
    public class QueueTimerManager : Singleton<QueueTimerManager>
    {
        public event Action<string> OnTaskStarted;
        public event Action<string> OnTaskCompleted;

        private Queue<TimerTask> taskQueue = new Queue<TimerTask>();
        private bool isRunning = false;
        private bool isPaused = false;
        private Coroutine queueCoroutine;

        public bool IsRunning => isRunning;
        public bool IsPaused => isPaused;

        public void EnqueueTask(TimerTaskData data)
        {
            taskQueue.Enqueue(new TimerTask(data));
            if (!isRunning && !isPaused)
            {
                queueCoroutine = StartCoroutine(RunQueue());
            }
        }

        public void CancelTaskById(string id)
        {
            taskQueue = new Queue<TimerTask>(taskQueue.Where(t => t.Id != id));
        }

        public void PauseQueue()
        {
            if (isRunning && !isPaused)
            {
                isPaused = true;
                StopCoroutine(queueCoroutine);
            }
        }

        public void ResumeQueue()
        {
            if (isPaused)
            {
                isPaused = false;
                queueCoroutine = StartCoroutine(RunQueue());
            }
        }

        public void ClearQueue()
        {
            StopAllCoroutines();
            taskQueue.Clear();
            isRunning = false;
            isPaused = false;
        }

        public List<string> GetPendingTasks()
        {
            return taskQueue.Select(t => t.Id).ToList();
        }

        public void ForceExecuteNext()
        {
            if (taskQueue.Count > 0)
            {
                TimerTask task = taskQueue.Dequeue();
                OnTaskStarted?.Invoke(task.Id);
                task.Callback?.Invoke();
                OnTaskCompleted?.Invoke(task.Id);
            }
        }

        private IEnumerator RunQueue()
        {
            isRunning = true;
            while (taskQueue.Count > 0)
            {
                TimerTask task = taskQueue.Dequeue();
                OnTaskStarted?.Invoke(task.Id);
                yield return new WaitForSeconds(task.DelaySeconds);
                task.Callback?.Invoke();
                OnTaskCompleted?.Invoke(task.Id);
            }
            isRunning = false;
        }

        public class TimerTaskData
        {
            public string Id { get; set; }
            public float DelaySeconds { get; set; }
            public Action Callback { get; set; }
            public string Tag { get; set; } = string.Empty;
        }

        private class TimerTask
        {
            public string Id { get; }
            public float DelaySeconds { get; }
            public Action Callback { get; }
            public string Tag { get; }

            public TimerTask(TimerTaskData data)
            {
                Id = data.Id;
                DelaySeconds = data.DelaySeconds;
                Callback = data.Callback;
                Tag = data.Tag;
            }
        }
    }
}
