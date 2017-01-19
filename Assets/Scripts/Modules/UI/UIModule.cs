/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIModule : Module, IUIModule
{	
	public StatsUIPanel StatsPanel;
	[SerializeField]
	UIElement levelText;
	[SerializeField]
	UIElement turnText;
	[SerializeField]
	UIButton endTurnButton;

	[Space(25)]
	[SerializeField]
	UIFillBar strengthBar;
	[SerializeField]
	UIFillBar skillBar;
	[SerializeField]
	UIFillBar magicBar;
	[SerializeField]
	UIFillBar constitutionBar;
	[SerializeField]
	UIFillBar speedBar;
	[SerializeField]
	UIFillBar critBar;

	[Space(25)]
	[SerializeField]
	UIButton menuButton;
	[SerializeField]
	UIElement healthDisplay;
	[SerializeField]
	UIElement unspentStatPoints;

	[Space(25)]
	[SerializeField]
	Image meleeIcon;
	[SerializeField]
	Image magicIcon;

	PlayerCharacterBehaviour playerAgent;
	PlayerCharacter playerUnit;
	UnitModule units;

	public void Init(string levelName, TurnModule turn, UnitModule units, TuningModule Tuning, bool createWorld = true) {
		this.units = units;
		levelText.SetText(levelName);
		turnText.SetText(turn.CurrentTurnStr());
		turn.SubscribeToTurnSwitchStr(handleTurnChange);
		endTurnButton.SubscribeToClick(turn.NextTurn);
		menuButton.SubscribeToClick(OpenMenuPopUp);
		if (createWorld) {
			this.playerAgent = units.GetMainPlayer ();
			this.playerAgent.SubscribeToAgilityChange (handleAgilityChange);
			this.playerUnit = playerAgent.GetUnit () as PlayerCharacter;
			updateHealthDisplay(playerAgent.Health());
			playerAgent.SubscribeToHPChange(updateHealthDisplay);
			playerAgent.SubscribeToEarnStatPoints(updateStatPonts);
			updateStatPonts(playerUnit.GetUnspentStatPoints());
		}
		if(StatsPanel) {
			StatsPanel.Init (this, Tuning, units);
			OpenMenuPopUp();
		}
	}

	public void UsePotion() {
		units.UsePotionOnPlayer();
	}

	public void OpenMenuPopUp () {
		StatsPanel.Show();
	}

	public void RefreshStats() {
		skillBar.SetText(playerUnit.GetSkill(), 1);
		strengthBar.SetText(playerUnit.GetStrength(), 1);
		magicBar.SetText(playerUnit.GetMagic(), 1);
		constitutionBar.SetText(playerUnit.GetConstitution(), 1);
		speedBar.SetText(playerUnit.GetSpeed(), 1);
		critBar.HandleUpdateFill(playerUnit.GetPlayerCritChanceAsPercentf());
	}

	void updateStatPonts (int numStatPoints) {
		unspentStatPoints.SetText(numStatPoints.ToString())	;
	}

	void updateHealthDisplay(int healthRemaining) { 
		healthDisplay.SetText(healthRemaining.ToString());
	}

	void handleAgilityChange(float newAgility) {
		speedBar.HandleUpdateFillValue(newAgility);
		speedBar.HandleUpdateFill(newAgility / (float) playerUnit.GetSpeed());
	}

	void handleTurnChange (string turnName) {
		turnText.SetText(turnName);
	}

	protected override void SubscribeEvents ()
	{
		base.SubscribeEvents ();
		EventModule.Subscribe(handlePODEvent);
	}

	protected override void UnusbscribeEvents ()
	{
		base.UnusbscribeEvents ();
		EventModule.Unsubscribe(handlePODEvent);
	}
		
	void handlePODEvent(PODEvent gameEvent) {
		switch(gameEvent) {
			case PODEvent.PlayerAttacked:
				meleeIcon.color = Color.red;
				magicIcon.color = Color.red;
				break;
			case PODEvent.PlayerTurnStart:
				meleeIcon.color = Color.white;
				magicIcon.color = Color.white;
				break;
			case PODEvent.EnemyTurnStart:
				endTurnButton.ToggleInteractable(false);
				break;
			case PODEvent.EnemyTurnEnd:
				endTurnButton.ToggleInteractable(true);
				break;
		}
	}

	void quitToMenu() {
		SceneManager.LoadScene(MAIN_MENU_INDEX);
	}

}
