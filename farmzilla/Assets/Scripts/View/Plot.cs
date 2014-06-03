using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class Plot : Button {
  public PlotBackground plotBackground;

  public void Start() {
    Sprite s = PlotSpriteLibrary.Instance.RandomSprite();
    _spriteRenderer.sprite = s;
    Normal = s;
    Hover = s;
    Active = s;
    Disabled = s;
    preferredTarget = s;

    IsDirty = true;
  }
}
