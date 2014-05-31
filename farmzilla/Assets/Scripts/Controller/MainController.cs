using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum Gamestate : uint {
  Title,
  HUD,
  GameOver
}

public class MainController : MonoBehaviour {
  public GameObject[] GameStates;

  protected GameObject CurrentGameState;

  public void Awake() {
    SwitchState( Gamestate.Title );
  }

  public void SwitchState( Gamestate state ) {
    for ( int i = 0, l = GameStates.Length; i < l; i++ ) {
      Debug.Log( "Will set " + i + " to be active? " + (i == (int)state) );
      if ( GameStates[i] != null ) {
        GameStates[i].SetActive( i == (int)state );
      }
    }
  }
}
