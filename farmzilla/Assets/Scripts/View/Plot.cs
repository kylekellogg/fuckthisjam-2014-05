using UnityEngine;
using System;
using System.Collections;

public class Plot : DisplayObject {
  protected MainController mainController;

  protected override void Initialize() {
    base.Initialize();

    mainController = FindObjectOfType<MainController>();

    _spriteRenderer.sprite = PlotSpriteLibrary.Instance.RandomSprite();
  }
}
