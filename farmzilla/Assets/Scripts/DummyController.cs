using UnityEngine;
using System.Collections;

public class DummyController : MonoBehaviour
{
	public Button MyButton;
	
		// Use this for initialization
		void Start ()
		{
			MyButton.MouseUp += HandleMouseUp;
			MyButton.MouseUp += HandleMouseUp2;
		}

		void HandleMouseUp2 (DisplayObject displayObject)
		{
		print ("BUTTDICK");
		}

		void HandleMouseUp (DisplayObject displayObject)
		{
			print ("Hello");
			MyButton.MouseUp -= HandleMouseUp;
			MyButton.MouseUp += HandleMouseUp1;
		}

		void HandleMouseUp1 (DisplayObject displayObject)
		{
		print ("FUCK OFF DICKBALLS");
		}
	
		// Update is called once per frame
		void Update ()
		{
		}
}

