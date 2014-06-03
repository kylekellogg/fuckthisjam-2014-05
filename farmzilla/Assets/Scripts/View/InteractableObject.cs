using UnityEngine;
using System.Collections;

public delegate void IEvent (InteractableObject interactableObject);

[RequireComponent (typeof (BoxCollider2D))]
public class InteractableObject : MonoBehaviour {

  protected IEvent MouseDownInvoker;
  public event IEvent MouseDown {
    add { MouseDownInvoker += value; }
    remove { MouseDownInvoker -= value; }
  }

  protected IEvent MouseUpInvoker;
  public event IEvent MouseUp {
    add { MouseUpInvoker += value; }
    remove { MouseUpInvoker -= value; }
  }

  protected IEvent MouseEnterInvoker;
  public event IEvent MouseEnter {
    add { MouseEnterInvoker += value; }
    remove { MouseEnterInvoker -= value; }
  }

  protected IEvent MouseExitInvoker;
  public event IEvent MouseExit {
    add { MouseExitInvoker += value; }
    remove { MouseExitInvoker -= value; }
  }

  public virtual void OnMouseEnter ()
  {
    if (MouseEnterInvoker != null) {
      MouseEnterInvoker(this);
    }
  }

  public virtual void OnMouseExit ()
  {
    if (MouseExitInvoker != null) {
      MouseExitInvoker(this);
    }
  }

  public virtual void OnMouseDown ()
  {
    if (MouseDownInvoker != null) {
      MouseDownInvoker(this);
    }
  }

  public virtual void OnMouseUp ()
  {
    if (MouseUpInvoker != null) {
      MouseUpInvoker(this);
    }
  }
}
