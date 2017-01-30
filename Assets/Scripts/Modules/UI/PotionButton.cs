/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class PotionButton : UIButton 
{	
	UIModule ui;

	protected override void SetReferences ()
	{
		base.SetReferences ();
		ui = GetComponentInParent<UIModule>();
	}

	protected override void executeClick ()
	{
		base.executeClick ();
		UsePotion();
	}

	public void UsePotion() {
		if(ui) {
			ui.UsePotion();
			ToggleInteractable(false);
		}
	}
}
