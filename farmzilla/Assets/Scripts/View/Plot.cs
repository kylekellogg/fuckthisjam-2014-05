using UnityEngine;
using System;
using System.Collections;

public enum PlotType : int {
  Burned,
  Burning,
  Destroyed,
  Initial,
  Upgraded1
}

public class Plot : InteractableObject {
  public DisplayObject Highlight;
  public DisplayObject PlotImage;
  public DisplayObject ProgressBar;
  public DisplayObject Flames;

  public bool IsEssential;

  protected bool hasActiveJob;
  protected Job activeJob;

  public PlotType type;

  protected System.Random myRandom;

  public void Start() {
    myRandom = new System.Random( (int)DateTime.Now.Ticks );
    if ( type == PlotType.Destroyed ) {
      double rnd = myRandom.NextDouble();
      //  1/3 chance to become burning
      if ( rnd > 0.32 && rnd < 0.67 ) {
        type = PlotType.Burning;
      }
    }
    PlotImage.sprite = PlotSpriteLibrary.Instance.SpriteForPlotType( type );
    PlotImage.IsDirty = true;

    Highlight.gameObject.SetActive( false );
    ProgressBar.gameObject.SetActive( false );

    MouseUp += HandleOnMouseUp;
  }

  public void Update() {
    if ( Highlight.gameObject.activeInHierarchy && Input.GetKeyDown( KeyCode.Return ) ) {
      Debug.Log( "Enter" );
      if ( !hasActiveJob && NotYetHighestLevel() ) {
        Debug.Log( "No active job, not highest level" );
        ScheduleNextJob();
      }
    }

    Flames.gameObject.SetActive( type == PlotType.Burning );
    if ( Flames.gameObject.activeInHierarchy ) {
      double rnd = myRandom.NextDouble();

      //  1/3 chance to change
      if ( rnd > 0.32 && rnd < 0.67 ) {
        Flames.sprite = PlotSpriteLibrary.Instance.Flames[ myRandom.Next( PlotSpriteLibrary.Instance.Flames.Length ) ];
      }
    }
  }

  protected bool NotYetHighestLevel() {
    if ( type == PlotType.Burning || type == PlotType.Destroyed || type == PlotType.Initial ) {
      return true;
    }
    //  Burned, Upgraded1
    return false;
  }

  protected void ScheduleNextJob() {
    switch ( type ) {
      case PlotType.Burned:
        return;
      case PlotType.Burning:
        activeJob = new Job( this, JobType.Save );
        break;
      case PlotType.Destroyed:
        activeJob = new Job( this, JobType.Build );
        break;
      case PlotType.Initial:
        activeJob = new Job( this, JobType.Upgrade1 );
        break;
      case PlotType.Upgraded1:
        return;
      default:
        return;
    }

    if ( activeJob == null ) {
      return;
    }

    activeJob.JobProgress += HandleJobProgress;
    activeJob.JobStarted += HandleJobStart;
    activeJob.JobComplete += HandleJobComplete;

    FindObjectOfType<JobQueue>().Enqueue( activeJob );
  }

  protected void HandleOnMouseUp( InteractableObject iobj ) {
    if ( type == PlotType.Destroyed || type == PlotType.Burning ) {
      ScheduleNextJob();
    } else {
      bool highlighted = !Highlight.gameObject.activeInHierarchy;
      Highlight.gameObject.SetActive( highlighted );

      Plot[] plots = FindObjectsOfType<Plot>();
      Plot goodPlot = this;
      Array.ForEach( plots, delegate( Plot plot ) {
        if ( plot != goodPlot ) {
          plot.Highlight.gameObject.SetActive( false );
        }
      } );
    }
  }

  protected void HandleJobProgress() {
    Vector3 origScale = ProgressBar.transform.localScale;
    origScale.x = activeJob.Progress;
    ProgressBar.transform.localScale = origScale;
  }

  protected void HandleJobStart() {
    ProgressBar.gameObject.SetActive( true );
    hasActiveJob = true;
  }

  protected void HandleJobComplete() {
    ProgressBar.gameObject.SetActive( false );

    hasActiveJob = false;

    PlotType origType = type;

    switch ( activeJob.Type ) {
      case JobType.Save:
        type = PlotType.Destroyed;
        break;
      case JobType.Build:
        type = PlotType.Initial;
        break;
      case JobType.Upgrade1:
        type = PlotType.Upgraded1;
        break;
      /*case JobType.Upgrade2:
        type = PlotType.Upgraded1;
        break;
      case JobType.Upgrade3:
        type = PlotType.Upgraded1;
        break;*/
      default:
        break;
    }

    if ( type != origType ) {
      PlotImage.sprite = PlotSpriteLibrary.Instance.SpriteForPlotType( type );
    }

    //  Cleanup
    activeJob.JobProgress -= HandleJobProgress;
    activeJob.JobStarted -= HandleJobStart;
    activeJob.JobComplete -= HandleJobComplete;
  }
}
