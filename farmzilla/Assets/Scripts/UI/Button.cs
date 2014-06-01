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

	private GUIStyle style;

	[SerializeField]
	protected string _label;
	public string Label {
		get {
			return _label;
		}
		set {
			_label = value;
			IsDirty = true;
		}
	}

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

	public void OnGUI () {
		Vector3 min = Camera.main.WorldToScreenPoint( _spriteRenderer.bounds.min );
		Vector3 max = Camera.main.WorldToScreenPoint( _spriteRenderer.bounds.max );

		Vector2 gmin = GUIUtility.ScreenToGUIPoint( new Vector2( min.x, Screen.height - min.y ) );
		Vector2 gmax = GUIUtility.ScreenToGUIPoint( new Vector2( max.x, Screen.height - max.y ) );

		/**
		* KK, 2014-06-01
		* 
		* Unity's coordinate system (-1, -1) as Top Left and (1, 1) as Top Right
		* mean that we must enter the Rect coordinates in a seemingly odd way
		* which is x = least x (min, therefore left)
		* and y = greatest y (max, therefore top)
		* The differences are likewise reversed w/ x being greatest - least
		* and y being least - greatest
		* 
		* The Screen.height - y coords above fix an offset issue that I'm not 100%
		* certain about. As it's working, please leave it.
		*/
		Rect rect = new Rect( gmin.x, gmax.y, gmax.x - gmin.x, gmin.y - gmax.y );
		GUI.Label( rect, Label, style );
	}

	protected override void Initialize ()
	{
		base.Initialize ();

		isOver = false;
		preferredTarget = Normal;

		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.richText = true;

		Label = "<color=#ffffff>"+Label+"</color>" ?? "";
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

