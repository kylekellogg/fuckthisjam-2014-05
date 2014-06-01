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

  public float IncomePerTimePeriod = 50.0f;
  public float IncomeTimePeriodInDays = 0.25f;
  public float Money = 0.0f;

  protected GameObject CurrentGameState;

  protected DateTime previousDateTime;

  public void Awake() {
    SwitchState( Gamestate.Title );

    previousDateTime = DateTime.Now;

    LogTime( "yesterday", TimeConverter.GameTimeSince( previousDateTime.AddDays( -1.0 ) ) );
    LogTime( "two days ago", TimeConverter.GameTimeSince( previousDateTime.AddDays( -2.0 ) ) );
    LogTime( "last week", TimeConverter.GameTimeSince( previousDateTime.AddDays( -7.0 ) ) );
    LogTime( "one hour ago", TimeConverter.GameTimeSince( previousDateTime.AddHours( -1.0 ) ) );
  }

  public void Update() {
    DateTime newNow = DateTime.Now;
    float income = MoneyEarnedForTimeSpan( newNow.Subtract( previousDateTime ) );
    Money += income;
    Debug.Log( "Have $" + Money );
    previousDateTime = newNow;
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
