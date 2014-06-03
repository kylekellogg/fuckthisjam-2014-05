using UnityEngine;
using System;
using System.Collections;

public class Plot : InteractableObject {
  public DisplayObject Highlight;
  public DisplayObject PlotImage;
  public DisplayObject ProgressBar;

  public bool IsDestroyed;
  public bool IsEssential;

  protected bool hasActiveJob;
  protected Job activeJob;

  public void Start() {
    PlotImage.sprite = IsDestroyed ? PlotSpriteLibrary.Instance.DestroyedSprite() : PlotSpriteLibrary.Instance.RandomSprite();
    PlotImage.IsDirty = true;

    Highlight.gameObject.SetActive( false );
    ProgressBar.gameObject.SetActive( false );

    MouseUp += HandleOnMouseUp;
  }

  protected void HandleOnMouseUp( InteractableObject iobj ) {
    if ( IsDestroyed ) {
      activeJob = new Job( this, JobType.Save );

      activeJob.JobProgress += delegate() {
        Vector3 origScale = ProgressBar.transform.localScale;
        origScale.x = activeJob.Progress;
        ProgressBar.transform.localScale = origScale;
      };

      activeJob.JobStarted += delegate() {
        ProgressBar.gameObject.SetActive( true );
        hasActiveJob = true;
      };

      activeJob.JobComplete += delegate() {
        ProgressBar.gameObject.SetActive( false );

        PlotImage.sprite = PlotSpriteLibrary.Instance.RandomSprite();
        hasActiveJob = false;
      };

      FindObjectOfType<JobQueue>().Enqueue( activeJob );
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
}
