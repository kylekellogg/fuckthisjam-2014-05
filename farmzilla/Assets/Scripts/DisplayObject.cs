using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (SpriteRenderer))]
public class DisplayObject : MonoBehaviour {
  protected SpriteRenderer _spriteRenderer;
  public SpriteRenderer spriteRenderer {
    get {
      return _spriteRenderer;
    }
  }

  [HideInInspector]
  public bool IsDirty;

  [HideInInspector]
  public bool IsInitialized {
    get;
    private set;
  }

  protected List<Transform> Children;

  private float _screenResolutionScale = (float)Screen.height / (float)Screen.width;

  public float Width {
    get {
      if ( _spriteRenderer.sprite != null ) {
        return _spriteRenderer.sprite.textureRect.width * _screenResolutionScale;
      }
      return 0f;
    }
  }

  public float Height {
    get {
      if ( _spriteRenderer.sprite != null ) {
        return _spriteRenderer.sprite.textureRect.height * _screenResolutionScale;
      }
      return 0f;
    }
  }

  public DisplayObject() {
    IsInitialized = false;
    Children = new List<Transform>();
  }

  public void Awake() {
    Initialize();
  }

  public virtual void Update() {
    DisplayObject[] displayObjects = GetComponentsInChildren<DisplayObject>() as DisplayObject[];
    for ( int i = 0, l = displayObjects.Length; i < l; i++ ) {
      IsDirty = IsDirty || displayObjects[i].IsDirty;
    }

    if ( IsDirty ) {
      Array.ForEach( displayObjects, delegate( DisplayObject dobj ) { dobj.IsDirty = true; } );
      UpdateView();
    }
  }

  protected virtual void Initialize() {
    if ( GetComponent<SpriteRenderer>() != null ) {
      _spriteRenderer = GetComponent<SpriteRenderer>();
    } else {
      _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    ResetSortingOrders( this.transform, _spriteRenderer );

    IsDirty = true;
    IsInitialized = true;
  }

  protected virtual void UpdateView() {
    IsDirty = false;
  }

  protected void ResetSortingOrders( Transform _transform, SpriteRenderer _renderer )
  {
    if ( _transform == null || _renderer == null )
      return;
    SpriteRenderer sr;
    foreach ( Transform t in _transform )
    {
      if ( t == null )
        continue;
      sr = t.GetComponent<SpriteRenderer>();
      if ( sr != null )
        sr.sortingOrder = Math.Max( sr.sortingOrder, _renderer.sortingOrder + 1 );
      if ( t.childCount > 0 )
        ResetSortingOrders( t, sr != null ? sr : _renderer );
    }
  }

  public virtual void AddChild( GameObject gameobject )
  {
    if ( gameobject == null )
      return;

    AddChild( gameobject.transform );
  }

  public virtual void AddChild( Transform t )
  {
    if ( t == null )
      return;

    /*SpriteRenderer sr = t.gameObject.GetComponent<SpriteRenderer>();
    if ( sr != null && spriteRenderer != null )
    {
      sr.sortingOrder = Math.Max( sr.sortingOrder, spriteRenderer.sortingOrder + 1 );
    }*/

    t.parent = transform;
    if ( !Children.Contains( t ) )
    {
      Children.Add( t );
    }
    else
    {
      Children.RemoveAt( Children.IndexOf( t ) );
      Children.Add( t );
    }

    ResetSortingOrders( transform, spriteRenderer );
  }

  public virtual GameObject RemoveChild( GameObject gameobject )
  {
    if ( gameobject == null )
      return null;

    RemoveChild( gameobject.transform );
    return gameobject;
  }

  public virtual Transform RemoveChild( Transform t )
  {
    if ( t == null )
      return null;

    if ( Children.Contains( t ) )
    {
      t.parent = null;
      Children.Remove( t );
    }
    else if ( t.parent == transform )
    {
      t.parent = null;
    }
    return t;
  }
}
