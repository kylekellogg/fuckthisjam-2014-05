using UnityEngine;
using System.Collections;

public class MoneyHUDComponent : HUDComponent {
  protected override void Initialize() {
    base.Initialize();

    Label = "<color=#ffffff>" + mainController.GetFormattedMoney() + "</color>";
  }

  public override void Update() {
    base.Update();

    Label = "<color=#ffffff>" + mainController.GetFormattedMoney() + "</color>";
  }
}
