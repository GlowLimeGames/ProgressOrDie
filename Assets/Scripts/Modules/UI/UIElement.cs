using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIElement : MonoBehaviourExtended {
	[SerializeField]
	int gameLevelIndex = 2;

	Image image;
	Text text;
	Text[] childTexts;

	[SerializeField]
	Sprite[] alternateSprites;
	public bool hasChildText {
		get {
			return childTexts != null;
		}
	}
	public bool hasImage {
		get {
			return image != null;
		}
	}
	public bool hasText {
		get {
			return text != null;
		}
	}
	public bool hasAlternateSprites {
		get {
			return alternateSprites.Length > 0;
		}
	}

	protected override void SetReferences() {
		base.SetReferences();
		image = GetComponentInChildren<Image>();
		text = GetComponentInChildren<Text>();
		childTexts = GetComponentsInChildren<Text>();
	}

	protected override void CleanupReferences()
	{
		// NOTHING
	}
		
	protected override void HandleNamedEvent(string eventName)
	{
		// NOTHING
	}

	public virtual void Show () {
		gameObject.SetActive(true);
	}

	public virtual void Hide () {
		gameObject.SetActive(false);
	}

	public void RandomSprite () {
		if (hasImage && hasAlternateSprites) {
			this.image.sprite = alternateSprites[Random.Range(0, alternateSprites.Length)];
		}
	}

	public void SetText(int value, int childIndex) {
		SetText(value.ToString(), childIndex);
	}

	public void SetText(string text, int childIndex) {
		CheckReferences();
		if(hasChildText && IntUtil.InRange(childIndex, childTexts.Length)) {
			childTexts[childIndex].text = text;
		}
	}

	public void SetText (string text) {
		CheckReferences();
		if (hasText) {
			this.text.text = text;
		}
	}

	public void SetText(int value) {
		SetText(value.ToString());
	}

	public void LoadLevel(string levelName) {
		PlayerPrefs.SetString(LEVEL, levelName);
		SceneManager.LoadScene(gameLevelIndex);
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene(CREDITS_INDEX);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void LoadMenu() {
		SceneManager.LoadScene(MAIN_MENU_INDEX);
	}
}
