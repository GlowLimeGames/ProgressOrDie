/*
 * Author(s): Ian Macpherson, Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */
using UnityEngine;
using UnityEngine.UI;


public class StatsUIPanel : UIElement
{
	const int CHARACTER_CREATION = 1;
	const int PLAYER_PROGRESSION = 0;
	[SerializeField]
	bool disableBattleButtonUntilPointsFull = false;
	UIModule parentModule;
	TuningModule Tuning;
	int Constitution = 0;
	int Skill = 0;
	int Strength = 0;
	int Spirit = 0;
	int Speed = 0;
	int StatsPanelMode = 1;
	Text ConstitutionText = null;
	Text SkillText = null;
	Text StrengthText = null;
	Text SpiritText = null;
	Text SpeedText = null;
	[SerializeField]
	Text pointsRemainingText;
	[SerializeField]
	UIButton battleButton;
	UnitModule unitModule;
	bool newCharacter;

	public void Init (UIModule ui, TuningModule Tuning, UnitModule units){
		this.parentModule = ui;
		this.Tuning = Tuning;
		unitModule = units;
		this.newCharacter = units.NewCharacter;
	}

	protected override void SetReferences ()
	{
		base.SetReferences ();
		battleButton.SubscribeToClick(Hide);
		battleButton.SubscribeToClick(switchToPlayMod);
	}
	protected void switchToPlayMod()
	{
		if(StatsPanelMode == CHARACTER_CREATION)
		{
			SwapPlayMode();
		}
	}
	protected override void FetchReferences ()
	{
		base.FetchReferences ();
		ConstitutionText = GameObject.Find("TextConstitution").GetComponent<Text>();
		SkillText = GameObject.Find("TextSkill").GetComponent<Text>();
		StrengthText = GameObject.Find("TextStrength").GetComponent<Text>();
		SpiritText = GameObject.Find("TextSpirit").GetComponent<Text>();
		SpeedText = GameObject.Find("TextSpeed").GetComponent<Text>();
		updateStats();
	}

	public override void Hide ()
	{
		base.Hide ();
		EventModule.Event(PODEvent.StatPanelClosed);
	}

	public void SwapPlayMode()
	{
		if (StatsPanelMode == 1) {
			StatsPanelMode = 0;
		}
		else {
			StatsPanelMode = 1;
		}
	}
	public override void Show ()
	{
		base.Show ();
		updateRemainingText();
	}
	void updateStats()
	{
		updateRemainingText();
		if(disableBattleButtonUntilPointsFull) {
			toggleBattleButton();
		}
		parentModule.RefreshStats();
	}

	void toggleBattleButton(){
		battleButton.ToggleInteractable(!HasUnllocatedStatPoints());
	}

	void updateRemainingText()
	{
		pointsRemainingText.text = GetUnallocatedStatPoints().ToString();
	}
	bool HasUnllocatedStatPoints(){
		return GetUnallocatedStatPoints() > 0;
	}
	int GetUnallocatedStatPoints()
	{
		return unitModule.GetAvailablePlayerSkillPoints();
	}
	int SumAllocatedStatPoints()
	{
		return Constitution + Skill + Strength + Spirit + Speed;
	}
	public void ConstitutionPlusClick()
	{
		if (!HasUnllocatedStatPoints()) {
			return;
		}
		Constitution = Constitution + 1;
		ConstitutionText.text = Constitution.ToString();
		unitModule.ChangePlayerConstitution (+1);
		unitModule.ChangePlayerUnspentStatPoints(-1);
		updateStats();
	}
	public void ConstitutionMinusClick()
	{
		if (Constitution == 0) {
			return;}
		Constitution = Constitution - 1;
		ConstitutionText.text = Constitution.ToString();
		unitModule.ChangePlayerConstitution (-1);
		unitModule.ChangePlayerUnspentStatPoints(1);
		updateStats();
	}
	public void SkillPlusClick()
	{
		if (Skill == Tuning.MaxSkill || !HasUnllocatedStatPoints()) {
			return;
		}
		Skill = Skill + 1;
		SkillText.text = Skill.ToString();
		unitModule.ChangePlayerSkill (1);
		unitModule.ChangePlayerUnspentStatPoints(-1);
		updateStats();
	}
	public void SkillMinusClick()
	{
		if (Skill == 0) {
			return;}
		Skill = Skill - 1;
		SkillText.text = Skill.ToString();
		unitModule.ChangePlayerSkill (-1);
		unitModule.ChangePlayerUnspentStatPoints(1);
		updateStats();
	}
	public void StrengthPlusClick()
	{
		if (!HasUnllocatedStatPoints()) {
			return;
		}
		Strength = Strength + 1;
		StrengthText.text = Strength.ToString();
		unitModule.ChangePlayerStrength (+1);
		unitModule.ChangePlayerUnspentStatPoints(-1);
		updateStats();
	}
	public void StrengthMinusClick()
	{
		if (Strength == 0) {
			return;}
		Strength = Strength - 1;
		StrengthText.text = Strength.ToString();
		unitModule.ChangePlayerStrength (-1);
		unitModule.ChangePlayerUnspentStatPoints(1);
		updateStats();
	}
	public void SpiritPlusClick()
	{
		if (!HasUnllocatedStatPoints()) {
			return;
		}
		Spirit = Spirit + 1;
		SpiritText.text = Spirit.ToString();
		unitModule.ChangePlayerMagic (1);
		unitModule.ChangePlayerUnspentStatPoints(-1);
		updateStats();
	}
	public void SpiritMinusClick()
	{
		if (Spirit == 0) {
			return;}
		Spirit = Spirit - 1;
		SpiritText.text = Spirit.ToString();
		unitModule.ChangePlayerMagic (-1);
		unitModule.ChangePlayerUnspentStatPoints(1);
		updateStats();
	}
	public void SpeedPlusClick()
	{
		if (!HasUnllocatedStatPoints()) {
			return;
		}
		if (StatsPanelMode == CHARACTER_CREATION && Speed >= Tuning.MaxSpeedInCharacterCreation) {
			return;
		}
		else if (Speed >= Tuning.MaxSpeed) {
			return;
		}
		Speed = Speed + 1;
		SpeedText.text = Speed.ToString();
		unitModule.ChangePlayerSpeed (1);
		unitModule.ChangePlayerUnspentStatPoints(-1);
		updateStats();
	}
	public void SpeedMinusClick()
	{
		if (Speed == 0) {
			return;}
		Speed = Speed - 1;
		SpeedText.text = Speed.ToString();
		unitModule.ChangePlayerSpeed (-1);
		unitModule.ChangePlayerUnspentStatPoints(1);
		updateStats();
	}
}