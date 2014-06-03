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

  protected JobQueue jobQueue;

  protected DateTime previousDateTime;

  public void OnApplicationPause() {
    Save();
  }

  public void OnApplicationFocus() {
    Save();
  }

  public void OnApplicationQuit() {
    Save();
  }

  public void Awake() {
    TimeConverter.Initialize();
    
    SwitchState( Gamestate.Title );

    Load();
    Debug.Log( "Starting with Money: " + string.Format( "{0:C}", Money ) );

    jobQueue = FindObjectOfType<JobQueue>();

    jobQueue.Enqueue( new Job( null ) );

    /*previousDateTime = DateTime.Now;
    DateTime yesterdayDateTime = previousDateTime.AddDays( -1.0 );
    TimeSpan yesterdayTimeSpan = TimeConverter.GameTimeSince( yesterdayDateTime );
    LogTime( "yesterday", yesterdayTimeSpan );
    LogTime( "yesterday game time", TimeConverter.RealTimeSince( previousDateTime - yesterdayTimeSpan ) );
    LogTime( "two days ago", TimeConverter.GameTimeSince( previousDateTime.AddDays( -2.0 ) ) );
    LogTime( "last week", TimeConverter.GameTimeSince( previousDateTime.AddDays( -7.0 ) ) );
    LogTime( "one hour ago", TimeConverter.GameTimeSince( previousDateTime.AddHours( -1.0 ) ) );*/
  }

  public void Update() {
    DateTime newNow = DateTime.Now;
    float income = MoneyEarnedForTimeSpan( newNow.Subtract( previousDateTime ) );
    Money += income;
//    Debug.Log( "Have $" + Money );
    previousDateTime = newNow;
  }

  public void SwitchState( Gamestate state ) {
    for ( int i = 0, l = GameStates.Length; i < l; i++ ) {
      if ( GameStates[i] != null ) {
        GameStates[i].SetActive( i == (int)state );
      }
    }
  }

  public float MoneyEarnedForTimeSpan( TimeSpan timespan ) {
    return (float)(timespan.TotalDays / IncomeTimePeriodInDays) * IncomePerTimePeriod;
  }

  public void Save() {
    PlayerPrefs.SetFloat( "Money", Money );
    PlayerPrefs.Save();
  }

  public void Load() {
    if ( PlayerPrefs.HasKey( "Money" ) ) {
      Money = PlayerPrefs.GetFloat( "Money" );
    }
  }

  public String GetFormattedMoney()
  {
        return string.Format("{0:C}", Money);
  }

  //  Debugging
  private void LogTime( string since, TimeSpan timespan ) {
    Debug.Log( "Game time since " + since + ": " + timespan.Days + " days, " + timespan.Hours + " hours, " + timespan.Minutes + " minutes, and " + timespan.Seconds + " seconds" );
    Debug.Log( "Earned: " + MoneyEarnedForTimeSpan( timespan ) );
  }
}
