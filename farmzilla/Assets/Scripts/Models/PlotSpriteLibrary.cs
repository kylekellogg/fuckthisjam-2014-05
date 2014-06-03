using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlotSpriteLibrary : MonoBehaviour {
  public static PlotSpriteLibrary Instance {get; private set;}

  public Sprite[] Sprites;

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
    int random = myRandom.Next( (int)Sprites.Length );
    Debug.Log( "Will attempt to get sprite at position: " + random );
    Debug.Log( Sprites[ random ] );
    return Sprites[random];
  }
}
