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
        job.Activate();
    }

	// Update is called once per frame
	void Update () {

        TimeSpan now = TimeConverter.GameTimeSince( DateTime.Now );

	    for ( int i = jobs.Count - 1; i > -1; i-- ) {
            Job job = jobs[i];
            job.Tick (now);
            
            if ( job.Progress >= 1.0f ) {
                job.Complete();
                jobs.RemoveAt(i);
            }
        }
	}
}
