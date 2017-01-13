/*
 * Author(s): Isaiah Mann
 * Description: Describes a simple button class
 * Usage: Attach to a GameObject with Unity Canvas button on it
 */

using UnityEngine;
using UnityEngine.UI;

public class UIFillBar : UIElement
{
	[SerializeField]
	Image fill;

	[SerializeField]
	Text barValue;

	bool hasBarValue {
		get {
			return barValue != null;
		}
	}

	public void HandleUpdateFillValue(float value) {
		if (hasBarValue) {
			barValue.text = value.ToString();
		}
	}

	public void HandleUpdateFill(float fillAmount) {
		fill.fillAmount = fillAmount;
	}
}
