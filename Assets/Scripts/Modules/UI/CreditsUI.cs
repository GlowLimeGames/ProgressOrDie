/*
 * Author(s): Isaiah Mann
 * Description: Returns to level select
 * Usage: [no notes]
 */

using UnityEngine.SceneManagement;

public class CreditsUI : UIElement 
{	

	public void LoadLevelSelect()
	{
		SceneManager.LoadScene(MAIN_MENU_INDEX);
	}

}
