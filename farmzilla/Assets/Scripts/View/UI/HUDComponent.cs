using UnityEngine;
using System.Collections;

public class HUDComponent : DisplayObject {

	protected MainController mainController;

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

	protected override void Initialize()
	{
		base.Initialize();
		mainController = FindObjectOfType<MainController>();

		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.richText = true;

		Label = !string.IsNullOrEmpty( Label ) ? "<color=#ffffff>" + Label + "</color>" : "";
	}
}
