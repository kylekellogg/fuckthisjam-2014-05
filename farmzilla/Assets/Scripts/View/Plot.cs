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

  public void Update() {
    if ( hasActiveJob ) {
      ProgressBar.gameObject.SetActive( true );

      Vector3 origScale = ProgressBar.transform.localScale;
      origScale.x = activeJob.Progress;
      ProgressBar.transform.localScale = origScale;
    } else {
      ProgressBar.gameObject.SetActive( false );
    }
  }

  protected void HandleOnMouseUp( InteractableObject iobj ) {
    if ( IsDestroyed ) {
      activeJob = new Job( this, JobType.Save );
      FindObjectOfType<JobQueue>().Enqueue( activeJob );
      hasActiveJob = true;
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
