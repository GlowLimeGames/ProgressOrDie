/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */
using UnityEngine;
using UnityEngine.UI;


public class StatsUIPanel : UIElement
{
	const int CHARACTER_CREATION = 1;
	const int PLAYER_PROGRESSION = 0;

	TuningModule Tuning;
	public void initTuning (TuningModule Tuning, UnitModule units){
		this.Tuning = Tuning;
		unitModule = units;
	}
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

	public UnitModule unitModule;

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
		
	public void SwapPlayMode()
	{
		if (StatsPanelMode == 1) {
			StatsPanelMode = 0;
		}
		else {
			StatsPanelMode = 1;
		}
	}
	void updateStats()
	{
		updateRemainingText();
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
		if(StatsPanelMode == CHARACTER_CREATION) {
			return Tuning.StartingStatPoints - SumAllocatedStatPoints();
		} else {
			return unitModule.GetAvailablePlayerSkillPoints();
		}
	}
	int SumAllocatedStatPoints()
	{
		return Constitution + Skill + Strength + Spirit + Speed;
	}
	public void ConstitutionPlusClick()
	{
		if (StatsPanelMode == 1) {
			if (!HasUnllocatedStatPoints()) {
				return;
			}
		}
		Constitution = Constitution + 1;
		ConstitutionText.text = Constitution.ToString();
		unitModule.ChangePlayerConstitution (+1);
		updateStats();
	}
	public void ConstitutionMinusClick()
	{
		if (Constitution == 0) {
			return;}
		Constitution = Constitution - 1;
		ConstitutionText.text = Constitution.ToString();
		unitModule.ChangePlayerConstitution (-1);
		updateStats();
	}
	public void SkillPlusClick()
	{
		if (Skill == Tuning.MaxSkill){
			return;}
		if (StatsPanelMode == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed > Tuning.StartingStatPoints) {
				return;
			}
		}
		Skill = Skill + 1;
		SkillText.text = Skill.ToString();
		unitModule.ChangePlayerSkill (Skill);
		updateStats();
	}
	public void SkillMinusClick()
	{
		if (Skill == 0) {
			return;}
		Skill = Skill - 1;
		SkillText.text = Skill.ToString();
		unitModule.ChangePlayerSkill (Skill);
		updateStats();
	}
	public void StrengthPlusClick()
	{
		if (StatsPanelMode == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed == Tuning.StartingStatPoints) {
				return;
			}
		}
		Strength = Strength + 1;
		StrengthText.text = Strength.ToString();
		unitModule.ChangePlayerStrength (Strength);
		updateStats();
	}
	public void StrengthMinusClick()
	{
		if (Strength == 0) {
			return;}
		Strength = Strength - 1;
		StrengthText.text = Strength.ToString();
		unitModule.ChangePlayerStrength (Strength);
		updateStats();
	}
	public void SpiritPlusClick()
	{
		if (StatsPanelMode == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed == Tuning.StartingStatPoints) {
				return;
			}
		}
		Spirit = Spirit + 1;
		SpiritText.text = Spirit.ToString();
		unitModule.ChangePlayerMagic (Spirit);
		updateStats();
	}
	public void SpiritMinusClick()
	{
		if (Spirit == 0) {
			return;}
		Spirit = Spirit - 1;
		SpiritText.text = Spirit.ToString();
		unitModule.ChangePlayerMagic (Spirit);
		updateStats();
	}
	public void SpeedPlusClick()
	{
		if (StatsPanelMode == 1) {
			if (Speed == Tuning.MaxSpeedInCharacterCreation) {
				return;
			}
			if (Constitution + Skill + Strength + Spirit + Speed == Tuning.StartingStatPoints) {
				return;
			}
		}
		if (Speed == Tuning.MaxSpeed) {
			return;}
		Speed = Speed + 1;
		SpeedText.text = Speed.ToString();
		unitModule.ChangePlayerSpeed (Speed);
		updateStats();
	}
	public void SpeedMinusClick()
	{
		if (Speed == 0) {
			return;}
		Speed = Speed - 1;
		SpeedText.text = Speed.ToString();
		unitModule.ChangePlayerSpeed (Speed);
		updateStats();
	}
}