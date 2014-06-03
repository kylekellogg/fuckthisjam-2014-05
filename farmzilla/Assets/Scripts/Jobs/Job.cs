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

public class Job {

    public TimeSpan CreatedAt;
    public TimeSpan WillBeCompletedOn;
    public Plot plot;

    public float Progress;
    protected bool isCompleted;
    protected bool isActive;

    protected TimeSpan lastTime;

    public Job (Plot pl, JobType jt)
    {
        isActive = false;
        isCompleted = false;
        plot = pl;
        Progress = 0f;
        CreatedAt = TimeConverter.GameTimeSince( DateTime.Now );
        TimeSpan completionTimeSpan = new TimeSpan( 0, (int)jt, 0, 0, 0 );
        WillBeCompletedOn = CreatedAt + completionTimeSpan;

        lastTime = CreatedAt;
    }

    public void Activate ()
    {
        isActive = true;
        isCompleted = false;
    }

    public void Complete ()
    {
        isCompleted = true;
        isActive = false;
    }

    public void Tick (TimeSpan gameTime)
    {
        TimeSpan offset = WillBeCompletedOn - gameTime;
        TimeSpan totalOffset = WillBeCompletedOn - CreatedAt;

        Progress = (float)((totalOffset.TotalHours - offset.TotalHours) / totalOffset.TotalHours);

        lastTime = gameTime;
    }
}
