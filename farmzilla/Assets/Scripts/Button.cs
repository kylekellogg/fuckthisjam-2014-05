using UnityEngine;
using System.Collections;

public delegate void UIEvent (DisplayObject displayObject);

[RequireComponent (typeof (BoxCollider2D))]
public class Button : DisplayObject
{
	public Sprite Normal;
	public Sprite Hover;
	public Sprite Active;
	public Sprite Disabled;
	public bool IsDisabled;

	private bool isOver;
	private bool isDown;

	protected Sprite _preferredTarget;
	public Sprite preferredTarget {
		get {
			return _preferredTarget;
		}

		set {
			_preferredTarget = value;
			IsDirty = true;
		}
	}

	protected UIEvent MouseDownInvoker;
	public event UIEvent MouseDown {
		add { MouseDownInvoker += value; }
		remove { MouseDownInvoker -= value; }
	}

	protected UIEvent MouseUpInvoker;
	public event UIEvent MouseUp {
		add { MouseUpInvoker += value; }
		remove { MouseUpInvoker -= value; }
	}

	protected UIEvent MouseEnterInvoker;
	public event UIEvent MouseEnter {
		add { MouseEnterInvoker += value; }
		remove { MouseEnterInvoker -= value; }
	}

	protected UIEvent MouseExitInvoker;
	public event UIEvent MouseExit {
		add { MouseExitInvoker += value; }
		remove { MouseExitInvoker -= value; }
	}

	protected override void Initialize ()
	{
		base.Initialize ();

		isOver = false;
		preferredTarget = Normal;
	}

	protected override void UpdateView ()
	{
		base.UpdateView ();

		if (IsDisabled) {
			_spriteRenderer.sprite = Disabled != null ? Disabled : preferredTarget;
		} else {
			_spriteRenderer.sprite = preferredTarget;
		}
	}

	public void OnMouseEnter ()
	{
		isOver = true;

		if(!isDown) {
			preferredTarget = Hover;
		}

		if (!IsDisabled) {
			if (MouseEnterInvoker != null) {
				MouseEnterInvoker(this);
			}
		}
	}

	public void OnMouseExit ()
	{
		isOver = false;

		if(!isDown) {
			preferredTarget = Normal;
		}

		if (!IsDisabled) {
			if (MouseExitInvoker != null) {
				MouseExitInvoker(this);
			}
		}
	}

	public void OnMouseDown ()
	{
		isDown = true;
		preferredTarget = Active;

		if (!IsDisabled) {
			if (MouseDownInvoker != null) {
				MouseDownInvoker(this);
			}
		}
	}

	public void OnMouseUp ()
	{
		isDown = false;

		if (isOver){
			preferredTarget = Hover;
		} else {
			preferredTarget = Normal;
		}

		if (!IsDisabled) {
			if (MouseUpInvoker != null) {
				MouseUpInvoker(this);
			}
		}
	}
}

