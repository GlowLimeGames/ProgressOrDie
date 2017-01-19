/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class GameOverUI : UIElement 
{	
	string lastLevelPlayed;
	protected override void SetReferences ()
	{
		base.SetReferences ();
		lastLevelPlayed = PlayerPrefs.GetString(LEVEL);
	}

	public void PlayAgain () {
		LoadLevel(lastLevelPlayed);
	}

}
