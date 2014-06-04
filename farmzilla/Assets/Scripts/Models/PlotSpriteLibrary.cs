using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlotSpriteLibrary : MonoBehaviour {
  public static PlotSpriteLibrary Instance {get; private set;}

  public Sprite[] Sprites;
  public Sprite[] Flames;

  protected System.Random myRandom;

	public void Awake() {
    if ( Instance ) {
      DestroyImmediate( this.gameObject );
    } else {
      DontDestroyOnLoad( this.gameObject );
      Instance = this;

      myRandom = new System.Random( (int)DateTime.Now.Ticks );
    }
  }

  public Sprite RandomSprite() {
    int random = myRandom.Next( (int)Sprites.Length - 2 ) + 2;
    return Sprites[ random ];
  }

  public Sprite DestroyedSprite() {
    return Sprites[ 1 ];
  }

  public Sprite SpriteForPlotType( PlotType type ) {
    switch ( type ) {
      case PlotType.Burned:
        return Sprites[ 0 ];
      case PlotType.Burning:
        return Sprites[ 0 ];
      case PlotType.Destroyed:
        return Sprites[ 1 ];
      case PlotType.Initial:
        return Sprites[ myRandom.Next( (int)Sprites.Length - 2 ) + 2 ];
      case PlotType.Upgraded1:
        return Sprites[ myRandom.Next( (int)Sprites.Length - 2 ) + 2 ];
      default:
        return Sprites[ 1 ];
    }
  }
}
