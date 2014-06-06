using UnityEngine;
using System;
using System.Collections;

public enum JobType : int {
    Save = 1,
    Build = 2,
    KaijuCooldown = 3,
    Upgrade1 = 4,
    Upgrade2 = 8,
    Upgrade3 = 16,
    KaijuAttack = 24,
    FirstKaijuAttack = 48
}

public delegate void JobCompleteCallback();
public delegate void JobProgressCallback();
public delegate void JobStartedCallback();

public class Job {

    public TimeSpan CreatedAt;
    public TimeSpan WillBeCompletedOn;

    public JobType Type;

    public float Progress;
    protected bool isCompleted;
    protected bool isActive;

    protected TimeSpan lastTime;

    protected System.Random myRandom;

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

    public Job (JobType jt)
    {
        isActive = false;
        isCompleted = false;
        Progress = 0f;
        myRandom = new System.Random( (int)DateTime.Now.Ticks );
        CreatedAt = TimeConverter.GameTimeSince( DateTime.Now );
#if UNITY_EDITOR
        TimeSpan completionTimeSpan = new TimeSpan( 0, 0, TimeForJobType( jt ), 0, 0 );
#else
        TimeSpan completionTimeSpan = new TimeSpan( 0, TimeForJobType( jt ), 0, 0, 0 );
#endif
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

    protected int TimeForJobType( JobType jt ) {
        if ( jt == JobType.KaijuAttack ) {
            int nextRando = myRandom.Next( 1, (int)jt );
            Debug.Log( "Next attack in: " + nextRando );
            return nextRando;
        }

        return (int)jt;
    }
}
