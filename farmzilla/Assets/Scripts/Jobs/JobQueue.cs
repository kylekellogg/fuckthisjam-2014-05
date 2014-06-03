using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class JobQueue : MonoBehaviour {

    protected List<Job> jobs;

    public JobQueue ()
    {
        jobs = new List<Job> ();
    }

    public void Enqueue (Job job)
    {
        jobs.Add (job);
    }

	// Update is called once per frame
	void Update () {

        TimeSpan now = TimeConverter.GameTimeSince( DateTime.Now );

	    foreach (Job job in jobs) {
            TimeSpan duration = job.WillBeCompletedOn.Subtract( now ).Duration();

            if (duration <= TimeSpan.Zero) {
                job.Complete();
                jobs.Remove(job);
            } else {
                job.Tick (now);
            }
        }
	}
}
