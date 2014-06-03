using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Plot : Button {
  public PlotBackground plotBackground;

  public bool IsDestroyed;
  public bool IsEssential;

  public void Start() {
    Sprite s = IsDestroyed ? PlotSpriteLibrary.Instance.DestroyedSprite() : PlotSpriteLibrary.Instance.RandomSprite();
    _spriteRenderer.sprite = s;
    Normal = s;
    Hover = s;
    Active = s;
    Disabled = s;
    preferredTarget = s;

    IsDirty = true;
  }
}
