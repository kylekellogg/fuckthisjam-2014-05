using UnityEngine;
using System;
using System.Collections;

public enum JobType : int {
    Save = 1,
    Build = 2,
    Upgrade1 = 4,
    Upgrade2 = 8,
    Upgrade3 = 16
}

public delegate void JobCompleteCallback();
public delegate void JobProgressCallback();
public delegate void JobStartedCallback();

public class Job {

    public TimeSpan CreatedAt;
    public TimeSpan WillBeCompletedOn;
    public Plot plot;

    public JobType Type;

    public float Progress;
    protected bool isCompleted;
    protected bool isActive;

    protected TimeSpan lastTime;

    protected JobCompleteCallback JobCompleteInvoker;
    public event JobCompleteCallback JobComplete {
        add { JobCompleteInvoker += value; }
        remove { JobCompleteInvoker -= value; }
    }

    protected JobProgressCallback JobProgressInvoker;
    public event JobProgressCallback JobProgress {
        add { JobProgressInvoker += value; }
        remove { JobProgressInvoker -= value; }
    }

    protected JobStartedCallback JobStartedInvoker;
    public event JobStartedCallback JobStarted {
        add { JobStartedInvoker += value; }
        remove { JobStartedInvoker -= value; }
    }

    public Job (Plot pl, JobType jt)
    {
        isActive = false;
        isCompleted = false;
        plot = pl;
        Progress = 0f;
        CreatedAt = TimeConverter.GameTimeSince( DateTime.Now );
        TimeSpan completionTimeSpan = new TimeSpan( 0, (int)jt, 0, 0, 0 );
        WillBeCompletedOn = CreatedAt + completionTimeSpan;

        Type = jt;

        lastTime = CreatedAt;
    }

    public void Activate ()
    {
        isActive = true;
        isCompleted = false;

        if ( JobStartedInvoker != null ) {
            JobStartedInvoker();
        }
    }

    public void Complete ()
    {
        isCompleted = true;
        isActive = false;

        if ( JobCompleteInvoker != null ) {
            JobCompleteInvoker();
        }
    }

    public void Tick (TimeSpan gameTime)
    {
        TimeSpan offset = WillBeCompletedOn - gameTime;
        TimeSpan totalOffset = WillBeCompletedOn - CreatedAt;

        Progress = (float)((totalOffset.TotalHours - offset.TotalHours) / totalOffset.TotalHours);
        if ( JobProgressInvoker != null ) {
            JobProgressInvoker();
        }

        lastTime = gameTime;
    }
}
