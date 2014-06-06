using UnityEngine;
using System;
using System.Collections;

public class GameController : BaseController {
  public HUDComponent Background;

  public float ScrollSpeedMultiplier = 1.0f;

  public Kaiju theKaiju;

  protected Plot[] Plots;

  protected Bounds BackgroundBounds;
  protected Transform BackgroundTransform;

  protected JobQueue jobQueue;
  protected Job nextKaijuJob;

	// Use this for initialization
	void Start () {
    BackgroundBounds = Background.spriteRenderer.renderer.bounds;
    BackgroundTransform = Background.transform;

    jobQueue = FindObjectOfType<JobQueue>();

    ScheduleKaijuAttack();
	}
	
	// Update is called once per frame
	void Update () {
    if ( Input.GetKey( KeyCode.DownArrow ) ) {
      BackgroundTransform.position += Vector3.up * ScrollSpeedMultiplier * Time.deltaTime;
    } else if ( Input.GetKey( KeyCode.UpArrow ) ) {
      BackgroundTransform.position += Vector3.down * ScrollSpeedMultiplier * Time.deltaTime;
    }

    if ( Input.GetKey( KeyCode.LeftArrow ) ) {
      BackgroundTransform.position += Vector3.right * ScrollSpeedMultiplier * Time.deltaTime;
    } else if ( Input.GetKey( KeyCode.RightArrow ) ) {
      BackgroundTransform.position += Vector3.left * ScrollSpeedMultiplier * Time.deltaTime;
    }

    Vector3 centerPosPx = Camera.main.WorldToScreenPoint( Vector3.zero );
    Vector3 backgroundPosPx = Camera.main.WorldToScreenPoint( BackgroundTransform.position );
    Vector3 backgroundSizePx = Camera.main.WorldToScreenPoint( BackgroundBounds.extents );

    float leftLimit = Screen.width + centerPosPx.x - backgroundSizePx.x;
    float rightLimit = centerPosPx.x + backgroundSizePx.x - Screen.width;
    float upLimit = Screen.height + centerPosPx.y - backgroundSizePx.y;
    float downLimit = centerPosPx.y + backgroundSizePx.y - Screen.height;

    if ( backgroundPosPx.x < leftLimit ) {
      backgroundPosPx.x = leftLimit;
    } else if ( backgroundPosPx.x > rightLimit ) {
      backgroundPosPx.x = rightLimit;
    }
    if ( backgroundPosPx.y < upLimit ) {
      backgroundPosPx.y = upLimit;
    } else if ( backgroundPosPx.y > downLimit ) {
      backgroundPosPx.y = downLimit;
    }

    BackgroundTransform.position = Camera.main.ScreenToWorldPoint( backgroundPosPx );
	}

  protected void ScheduleFirstKaijuAttack() {
    nextKaijuJob = new Job( JobType.FirstKaijuAttack );
    AddToQueue();
  }

  protected void ScheduleKaijuAttack() {
    nextKaijuJob = new Job( JobType.KaijuAttack );
    AddToQueue();
  }

  protected void ScheduleKaijuCooldown() {
    nextKaijuJob = new Job( JobType.KaijuCooldown );
    AddToQueue();
  }

  protected void AddToQueue() {
    nextKaijuJob.JobStarted += KaijuJobStart;
    nextKaijuJob.JobProgress += KaijuJobProgress;
    nextKaijuJob.JobComplete += KaijuJobComplete;

    jobQueue.Enqueue( nextKaijuJob );
  }

  protected void KaijuJobStart() {
    Debug.Log( "Job started" );
  }

  protected void KaijuJobProgress() {
    Debug.Log( "Job " + nextKaijuJob.Progress + " done" );
  }

  protected void KaijuJobComplete() {
    nextKaijuJob.JobStarted -= KaijuJobStart;
    nextKaijuJob.JobProgress -= KaijuJobProgress;
    nextKaijuJob.JobComplete -= KaijuJobComplete;

    if ( nextKaijuJob.Type != JobType.KaijuAttack ) {
      theKaiju.StopAttack();

      ScheduleKaijuAttack();
    } else {
      theKaiju.Attack();

      ScheduleKaijuCooldown();
    }
  }
}
