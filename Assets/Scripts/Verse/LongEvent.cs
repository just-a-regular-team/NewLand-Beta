using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LongEvent
{
   public class QueuedLongEvent
    {
        public Action eventAction;
        public IEnumerator eventActionEnumerator;
        public string sceneToLoad;
        public string eventTextKey = "";//nameEvent
        public string eventText = "";//nameEventAfterTrans
        public bool doAsynchronously;
        public Action<Exception> exceptionHandler;
        public bool alreadyDisplayed;
        public bool canEverUseStandardWindow = true;
        public bool showExtraUIInfo = true;

        public bool UseAnimatedDots
        {
            get
            {
                return this.doAsynchronously || this.eventActionEnumerator != null;
            }
        }

        public bool ShouldWaitUntilDisplayed
        {
            get
            {
                if (!alreadyDisplayed && UseStandardWindow)
                {
                    return !string.IsNullOrEmpty(eventText);
                }
                return false;
            }
        }

        public bool UseStandardWindow
        {
            get
            {
                return (canEverUseStandardWindow && !doAsynchronously && eventActionEnumerator == null);
            }
        }
    }
    private static Queue<QueuedLongEvent> eventQueue = new Queue<QueuedLongEvent>();
    public static Queue<QueuedLongEvent> EventBeforQuitGame = new Queue<QueuedLongEvent>();

    private static QueuedLongEvent currentEvent = null;

    private static Thread eventThread = null;

    private static AsyncOperation levelLoadOp = null;

    private static List<Action> toExecuteWhenFinished = new List<Action>();

    private static bool executingToExecuteWhenFinished = false;

    private static readonly object CurrentEventTextLock = new object();

    //private static readonly Vector2 StatusRectSize = new Vector2(240f, 75f);

    public static bool ShouldWaitForEvent
    {
        get
        {
            return AnyEventNowOrWaiting && (currentEvent.UseStandardWindow || (Current.SceneRoot.uiRoot == null || Current.SceneRoot.uiRoot.windowUI == null));
        }
    }

    public static bool AnyEventNowOrWaiting
    {
        get
        {
            return currentEvent != null || eventQueue.Count > 0;
        }
    }
    public static bool AnyEventWhichDoesntUseStandardWindowNowOrWaiting
    {
        get
        {
            QueuedLongEvent queuedLongEvent = currentEvent;
            if (queuedLongEvent != null && !queuedLongEvent.UseStandardWindow)
            {
                return true;
            }
            return eventQueue.Any((QueuedLongEvent x) => !x.UseStandardWindow);
        }
    }
    public static bool ForcePause => AnyEventNowOrWaiting;

    public static void QueueLongEvent(Action action, string textKey, bool doAsynchronously, Action<Exception> exceptionHandler, bool showExtraUIInfo = true)
    {
        QueuedLongEvent queuedLongEvent = new QueuedLongEvent();
        queuedLongEvent.eventAction = action;
        queuedLongEvent.eventTextKey = textKey;
        queuedLongEvent.doAsynchronously = doAsynchronously;
        queuedLongEvent.exceptionHandler = exceptionHandler;
        queuedLongEvent.canEverUseStandardWindow = !AnyEventWhichDoesntUseStandardWindowNowOrWaiting;
        queuedLongEvent.showExtraUIInfo = showExtraUIInfo;
        eventQueue.Enqueue(queuedLongEvent);
    }

    public static void QueueLongEvent(IEnumerable action, string textKey, Action<Exception> exceptionHandler = null, bool showExtraUIInfo = true)
    {
        QueuedLongEvent queuedLongEvent = new QueuedLongEvent();
        queuedLongEvent.eventActionEnumerator = action.GetEnumerator();
        queuedLongEvent.eventTextKey = textKey;
        queuedLongEvent.doAsynchronously = false;
        queuedLongEvent.exceptionHandler = exceptionHandler;
        queuedLongEvent.canEverUseStandardWindow = !AnyEventWhichDoesntUseStandardWindowNowOrWaiting;
        queuedLongEvent.showExtraUIInfo = showExtraUIInfo;
        eventQueue.Enqueue(queuedLongEvent);
    }

    public static void QueueLongEvent(Action preLoadLevelAction, string sceneToLoad, string textKey, bool doAsynchronously, Action<Exception> exceptionHandler)
    {
        QueuedLongEvent queuedLongEvent = new QueuedLongEvent();
        queuedLongEvent.eventAction = preLoadLevelAction;
        queuedLongEvent.sceneToLoad = sceneToLoad;
        queuedLongEvent.eventTextKey = textKey;
        queuedLongEvent.doAsynchronously = doAsynchronously;
        queuedLongEvent.exceptionHandler = exceptionHandler;
        queuedLongEvent.canEverUseStandardWindow = !AnyEventWhichDoesntUseStandardWindowNowOrWaiting;
        eventQueue.Enqueue(queuedLongEvent);
    }

    public static void ClearQueuedEvents()
    {
        eventQueue.Clear();
    }






    public static void UpdateLongEventGUI()
    {

    }
    public static void UpdateLongEvent(out bool sceneChanged)
    {
        sceneChanged = false;
        if (currentEvent != null)
        {
            if (currentEvent.eventActionEnumerator != null)
            {
                UpdateCurrentEnumeratorEvent();
            }
            else if (currentEvent.doAsynchronously)
            {
                UpdateCurrentAsynchronousEvent();
            }
            else
            {
                UpdateCurrentSynchronousEvent(out sceneChanged);
            }
        }
        if (currentEvent == null && eventQueue.Count > 0)
        {
            currentEvent = eventQueue.Dequeue();
             
            Debug.LogWarning(currentEvent.eventTextKey);
            if (currentEvent.eventTextKey == null)
            {
                currentEvent.eventText = "";
            }
            else
            {
                currentEvent.eventText = currentEvent.eventTextKey; //.Translate();
            }
        }
    }

    public static void ExecuteWhenFinished(Action action)
    {
        toExecuteWhenFinished.Add(action);
        if ((currentEvent == null || currentEvent.ShouldWaitUntilDisplayed) && !executingToExecuteWhenFinished)
        {
            ExecuteToExecuteWhenFinished();
        }
    }

    public static void SetCurrentEventText(string newText)
    {
        lock (CurrentEventTextLock)
        {
            if (currentEvent != null)
            {
                currentEvent.eventText = newText;
            }
        }
    }


    private static void UpdateCurrentEnumeratorEvent()
    {
        try
        {
            float num = Time.realtimeSinceStartup + 0.1f;
            do
            {
                if (!currentEvent.eventActionEnumerator.MoveNext())
                {
                    if (currentEvent.eventActionEnumerator is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    currentEvent = null;
                    eventThread = null;
                    levelLoadOp = null;
                    ExecuteToExecuteWhenFinished();
                    break;
                }
            }
            while (!(num <= Time.realtimeSinceStartup));
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception from long event: " + ex);
            if (currentEvent != null)
            {
                if (currentEvent.eventActionEnumerator is IDisposable disposable2)
                {
                    disposable2.Dispose();
                }
                if (currentEvent.exceptionHandler != null)
                {
                    currentEvent.exceptionHandler(ex);
                }
            }
            currentEvent = null;
            eventThread = null;
            levelLoadOp = null;
        }
    }

    private static void UpdateCurrentAsynchronousEvent()
    {
        if (eventThread == null)
        {
            eventThread = new Thread((ThreadStart)delegate
            {
                RunEventFromAnotherThread(currentEvent.eventAction);
            });
            eventThread.Start();
        }
        else
        {
            if (eventThread.IsAlive)
            {
                return;
            }
            bool flag = false;
            if (!string.IsNullOrEmpty(currentEvent.sceneToLoad))
            {
                if (levelLoadOp == null)
                {
                    levelLoadOp = SceneManager.LoadSceneAsync(currentEvent.sceneToLoad);
                }
                else if (levelLoadOp.isDone)
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                currentEvent = null;
                eventThread = null;
                levelLoadOp = null;
                ExecuteToExecuteWhenFinished();
            }
        }
    }

    private static void UpdateCurrentSynchronousEvent(out bool sceneChanged)
    {
        sceneChanged = false;
        if (currentEvent.ShouldWaitUntilDisplayed)
        {
            Debug.LogError("UpdateCurrentSynchronousEvent Return");
            return;
        }
        try
        {
            if (!string.IsNullOrEmpty(currentEvent.sceneToLoad))
            { 
                SceneManager.LoadScene(currentEvent.sceneToLoad);
                sceneChanged = true;
            }
            if (currentEvent.eventAction != null)//changed
            { 
                currentEvent.eventAction();
            }
            currentEvent = null;
            eventThread = null;
            levelLoadOp = null;
            ExecuteToExecuteWhenFinished();
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception from long event: " + ex);
            if (currentEvent != null && currentEvent.exceptionHandler != null)
            {
                currentEvent.exceptionHandler(ex);
            }
            currentEvent = null;
            eventThread = null;
            levelLoadOp = null;
        }
    }

    private static void RunEventFromAnotherThread(Action action)
    {
         
        try
        {
            action?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception from asynchronous event: " + ex);
            try
            {
                if (currentEvent != null && currentEvent.exceptionHandler != null)
                {
                    currentEvent.exceptionHandler(ex);
                }
            }
            catch (Exception ex2)
            {
                Debug.LogError("Exception was thrown while trying to handle exception. Exception: " + ex2);
            }
        }
    }

    private static void ExecuteToExecuteWhenFinished()
    {
        if (executingToExecuteWhenFinished)
        {
            Debug.LogError("Already executing.");
            return;
        }
        executingToExecuteWhenFinished = true;
         
        for (int i = 0; i < toExecuteWhenFinished.Count; i++)
        {
             
            try
            {
                toExecuteWhenFinished[i]();
            }
            catch (Exception ex)
            {
                Debug.LogError("Could not execute post-long-event action. Exception: " + ex);
            }
            finally
            {
                 
            }
        }
        toExecuteWhenFinished.Clear();
        executingToExecuteWhenFinished = false;
    }



    
}
