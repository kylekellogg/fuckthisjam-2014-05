using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class HUDComponent : DisplayObject {

	public MainController MainController;

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

	public void OnGUI () {
		Vector3 rawMin = _spriteRenderer.bounds.min;
		Vector3 rawMax = _spriteRenderer.bounds.max;

		rawMin.y *= -1;
		rawMax.y *= -1;

		Vector3 min = Camera.main.WorldToScreenPoint( rawMin );
		Vector3 max = Camera.main.WorldToScreenPoint( rawMax );

		Debug.Log ("Min: " + min.ToString());
		Debug.Log ("Max: " + max.ToString());

		Rect rect = new Rect( min.x, min.y, max.x - min.x, max.y - min.y );
		GUI.Label( rect, Label, style );
	}

	protected override void Initialize()
	{
		base.Initialize();

		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.richText = true;

		Label = "<color=#ffffff>" + MainController.Money + "</color>";
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		base.Update ();
		Label = "<color=#ffffff>" + MainController.Money + "</color>";
	}

	protected override void UpdateView ()
	{
		base.UpdateView ();
	}
}
