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

  public float IncomePerTimePeriod = 10.0f;
  public float IncomeTimePeriodInDays = 0.25f;

  protected GameObject CurrentGameState;

  public void Awake() {
    SwitchState( Gamestate.Title );

    LogTime( "yesterday", TimeConverter.GameTimeSince( DateTime.Now.AddDays( -1.0 ) ) );
    LogTime( "two days ago", TimeConverter.GameTimeSince( DateTime.Now.AddDays( -2.0 ) ) );
    LogTime( "last week", TimeConverter.GameTimeSince( DateTime.Now.AddDays( -7.0 ) ) );
    LogTime( "one hour ago", TimeConverter.GameTimeSince( DateTime.Now.AddHours( -1.0 ) ) );
  }

  public void SwitchState( Gamestate state ) {
    for ( int i = 0, l = GameStates.Length; i < l; i++ ) {
      Debug.Log( "Will set " + i + " to be active? " + (i == (int)state) );
      if ( GameStates[i] != null ) {
        GameStates[i].SetActive( i == (int)state );
      }
    }
  }

  public float MoneyEarnedForTimeSpan( TimeSpan timespan ) {
    return (float)(timespan.TotalDays / IncomeTimePeriodInDays) * IncomePerTimePeriod;
  }

  private void LogTime( string since, TimeSpan timespan ) {
    Debug.Log( "Game time since " + since + ": " + timespan.Days + " days, " + timespan.Hours + " hours, " + timespan.Minutes + " minutes, and " + timespan.Seconds + " seconds" );
    Debug.Log( "Earned: " + MoneyEarnedForTimeSpan( timespan ) );
  }
}
