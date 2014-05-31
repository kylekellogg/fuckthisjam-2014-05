using UnityEngine;
using System.Collections;

public class Label : DisplayObject
{

	public Sprite LabelSprite;

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

	protected override void Initialize ()
	{
		base.Initialize ();

		preferredTarget = LabelSprite;
	}
	
	protected override void UpdateView ()
	{
		base.UpdateView ();
		_spriteRenderer.sprite = preferredTarget ?? null;
	}
}