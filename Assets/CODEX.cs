using UnityEngine;
using UnityEngine.UI;

public class CODEX : MonoBehaviour {
	public Image frame;
	public Button backwardButton, forwardButton;
	public Sprite[] pictures;
	int currentIndex = 0;

	void Start () {
		frame.sprite = pictures [currentIndex];
		Button backBtn = backwardButton.GetComponent<Button>();
		Button forwardBtn = forwardButton.GetComponent<Button>();
		backBtn.onClick.AddListener(backwardClick);
		forwardBtn.onClick.AddListener(forwardClick);

	}

	public void forwardClick () {
		if (Input.GetMouseButtonDown (0)) {
			currentIndex++;
			currentIndex %= pictures.Length;
			frame.sprite = pictures [currentIndex];
		}

	}

	public void backwardClick() {
		if(Input.GetMouseButtonDown (0) && currentIndex > 0 ) {
			currentIndex--;
			frame.sprite = pictures[currentIndex];
		}

		else if(Input.GetMouseButtonDown(0) && currentIndex == 0) {
			currentIndex = pictures.Length - 1;
			frame.sprite = pictures[currentIndex];
		}
	}

}
