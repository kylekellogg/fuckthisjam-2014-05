using UnityEngine;
using System.Collections;

public class TitleController : BaseController {

	public Button PlayButton;

	// Use this for initialization
	void Start () {
		PlayButton.MouseDown += HandleMouseDown;
	}

	void HandleMouseDown (DisplayObject displayObject)
	{
		MainController.SwitchState(Gamestate.HUD);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
