/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : UIElement 
{	
	[SerializeField]
	int gameLevelIndex = 1;

	public void LoadLevel(string levelName) {
		PlayerPrefs.SetString(LEVEL, levelName);
		SceneManager.LoadScene(gameLevelIndex);
	}

	public void QuitGame(){
		Application.Quit();
	}

}
