using UnityEngine;
using System;
using System.Collections;

public class Kaiju : DisplayObject {
  protected System.Random myRandom;

  protected override void Initialize() {
    base.Initialize();

    myRandom = new System.Random( (int)DateTime.Now.Ticks );
  }

  public override void Update() {
    base.Update();

    //Vector3 loc = transform.position;

    double rnd = myRandom.NextDouble();

    if ( rnd > 0.124 && rnd < 0.26 ) {
      float curRot = transform.rotation.eulerAngles.z;
      int minRot = (int)curRot - 15;
      int maxRot = minRot + 30;
      float newRot = (float)myRandom.Next(minRot, maxRot);

      transform.rotation = Quaternion.Euler( 0f, 0f, newRot );
    }

    /*if ( myRandom.NextDouble() > 0.65 ) {
      float rnd = (float)myRandom.NextDouble();
      float cur = (float)rot.z;
      int min = (int)cur - 15;
      if ( min < 0 ) {
        min = 0;
      }
      int max = min + 30;
      float next = (float)myRandom.Next( min, max );
      //rot.z = rnd * next;
      rot.z = 1.0f;
      //Debug.Log( "Went from " + cur + " to " + rot.z );
      //Debug.Log( "rnd: " + rnd + " min: " + min + " max: " + max + " next: " + next );

      transform.rotation = rot;
    }*/
  }
}
