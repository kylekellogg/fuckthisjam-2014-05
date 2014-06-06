using UnityEngine;
using System;
using System.Collections;

public class Kaiju : DisplayObject {
  public float SpeedDampening = 0.1f;

  protected System.Random myRandom;

  protected Vector3 originalPosition;

  protected override void Initialize() {
    base.Initialize();

    myRandom = new System.Random( (int)DateTime.Now.Ticks );
  }

  public void Start() {
    originalPosition = transform.position;
  }

  public override void Update() {
    if ( gameObject.activeInHierarchy ) {
      base.Update();

      double rnd = myRandom.NextDouble();

      if ( rnd > 0.124 && rnd < 0.26 ) {
        float curRot = transform.rotation.eulerAngles.z;
        int minRot = (int)curRot - 15;
        int maxRot = minRot + 30;
        float newRot = (float)myRandom.Next(minRot, maxRot);

        transform.rotation = Quaternion.Euler( 0f, 0f, newRot );
      }

      transform.position = transform.position + (transform.up * Time.deltaTime * SpeedDampening);
    } else {
      transform.position = originalPosition;
    }
  }

  public void OnTriggerEnter2D( Collider2D collider ) {
    Debug.Log( "Trigger Enter: " + collider );
  }

  public void OnTriggerStay2D( Collider2D collider ) {
    Debug.Log( "Trigger Stay: " + collider );
  }

  public void OnTriggerExit2D( Collider2D collider ) {
    Debug.Log( "Trigger Exit: " + collider );
  }

  public void OnCollisionEnter2D( Collision2D collision ) {
    Debug.Log( "Collision Enter: " + collision );
  }

  public void OnCollisionStay2D( Collision2D collision ) {
    Debug.Log( "Collision Stay: " + collision );
  }

  public void OnCollisionExit2D( Collision2D collision ) {
    Debug.Log( "Collision Exit: " + collision );
  }

  public void Attack() {
    transform.position = originalPosition;
    gameObject.SetActive( true );
  }

  public void StopAttack() {
    gameObject.SetActive( false );
  }
}
