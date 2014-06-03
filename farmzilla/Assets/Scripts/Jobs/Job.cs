using UnityEngine;
using System;
using System.Collections;

public class Job : MonoBehaviour {

    public TimeSpan CreatedAt;
    public TimeSpan WillBeCompletedOn;
    public DisplayObject plot;

    protected float progress;
    protected bool isCompleted;
    protected bool isActive;

    void Schedule (DisplayObject pl)
    {
        isActive = false;
        isCompleted = false;
        plot = pl;
        progress = 0f;
        CreatedAt = TimeConverter.GameTimeSince( DateTime.Now );
        WillBeCompletedOn = TimeConverter.GameTimeSince( DateTime.Now.AddHours(16) );
    }

    public void Activate ()
    {
        isActive = true;
    }

    public void Complete ()
    {
        isCompleted = true;
    }

    public void Tick (TimeSpan gameTime)
    {
        TimeSpan startOffset = gameTime - CreatedAt;
        TimeSpan endOffset = WillBeCompletedOn - CreatedAt;

        progress = (float) (startOffset - endOffset).TotalHours;
    }
}
