using UnityEngine;
using System;
using System.Collections;

public class Job {

    public TimeSpan CreatedAt;
    public TimeSpan WillBeCompletedOn;
    public DisplayObject plot;

    public float Progress;
    protected bool isCompleted;
    protected bool isActive;

    protected TimeSpan lastTime;

    public Job (DisplayObject pl)
    {
        isActive = false;
        isCompleted = false;
        plot = pl;
        Progress = 0f;
        CreatedAt = TimeConverter.GameTimeSince( DateTime.Now );
        TimeSpan completionTimeSpan = new TimeSpan( 0, 0, 1, 0, 0 );
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
