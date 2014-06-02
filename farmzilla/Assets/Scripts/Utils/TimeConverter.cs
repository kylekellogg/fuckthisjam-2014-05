using UnityEngine;
using System;
using System.Collections;

public class TimeConverter {
  //  8 3-hour days per 24 hour period
  public const float DAY_CONVERSION_RATE = 8.0f;

  public static TimeSpan GameTimeSince( DateTime date ) {
    DateTime now = DateTime.Now;
    TimeSpan diff = now.Subtract( date );
    double totalDays = diff.TotalDays * DAY_CONVERSION_RATE;
    Debug.Log( "Time->GameTime Conversion: " + diff.TotalDays.ToString() + " will become " + totalDays.ToString() + " in-game days" );
    return TimeSpan.FromDays( totalDays );
  }

  public static TimeSpan RealTimeSince( DateTime gamedate ) {
    DateTime now = DateTime.Now;
    TimeSpan diff = now.Subtract( gamedate );
    double totalDays = diff.TotalDays / DAY_CONVERSION_RATE;
    Debug.Log( "GameTime->Time Conversion: " + diff.TotalDays.ToString() + " will become " + totalDays.ToString() + " real days" );
    return TimeSpan.FromDays( totalDays );
  }
}
