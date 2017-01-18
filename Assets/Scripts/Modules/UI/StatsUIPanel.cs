/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */
using UnityEngine;
using UnityEngine.UI;


public class StatsUIPanel : UIElement
	
{
	UnitModule units;
	TuningModule Tuning;
	public void initTuning (TuningModule Tuning, UnitModule units){
		this.Tuning = Tuning;
		this.units = units;
		ChangeStat = units;
	}

	int Constitution = 0;
	int Skill = 0;
	int Strength = 0;
	int Spirit = 0;
	int Speed = 0;
	int IfCreation = 1;
	Text ConstitutionText = null;
	Text SkillText = null;
	Text StrengthText = null;
	Text SpiritText = null;
	Text SpeedText = null;
	public UnitModule ChangeStat;

	void Start ()
	{
	ConstitutionText = GameObject.Find("TextConstitution").GetComponent<Text>();
	SkillText = GameObject.Find("TextSkill").GetComponent<Text>();
	StrengthText = GameObject.Find("TextStrength").GetComponent<Text>();
	SpiritText = GameObject.Find("TextSpirit").GetComponent<Text>();
	SpeedText = GameObject.Find("TextSpeed").GetComponent<Text>();
	}

	public void SwapPlayMode()
	{
		if (IfCreation == 1) {
			IfCreation = 0;
		}
		else {
			IfCreation = 1;
		}
	}

	public void ConstitutionPlusClick()
	{
		if (IfCreation == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed == Tuning.StartingStatPoints) {
				return;
			}
		}
		Constitution = Constitution + 1;
		ConstitutionText.text = Constitution.ToString();
		ChangeStat.ChangePlayerConstitution (+1);
		print (Constitution);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void ConstitutionMinusClick()
	{
		if (Constitution == 0) {
			return;}
		Constitution = Constitution - 1;
		ConstitutionText.text = Constitution.ToString();
		ChangeStat.ChangePlayerConstitution (-1);
		print (Constitution);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void SkillPlusClick()
	{
		if (Skill == Tuning.MaxSkill){
			return;}
		if (IfCreation == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed > Tuning.StartingStatPoints) {
				return;
			}
		}
		Skill = Skill + 1;
		SkillText.text = Skill.ToString();
		ChangeStat.ChangePlayerSkill (Skill);
		print (Skill);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void SkillMinusClick()
	{
		if (Skill == 0) {
			return;}
		Skill = Skill - 1;
		SkillText.text = Skill.ToString();
		ChangeStat.ChangePlayerSkill (Skill);
		print (Skill);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void StrengthPlusClick()
	{
		if (IfCreation == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed == Tuning.StartingStatPoints) {
				return;
			}
		}
		Strength = Strength + 1;
		StrengthText.text = Strength.ToString();
		ChangeStat.ChangePlayerStrength (Strength);
		print (Strength);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void StrengthMinusClick()
	{
		if (Strength == 0) {
			return;}
		Strength = Strength - 1;
		StrengthText.text = Strength.ToString();
		ChangeStat.ChangePlayerStrength (Strength);
		print (Strength);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void SpiritPlusClick()
	{
		if (IfCreation == 1) {
			if (Constitution + Skill + Strength + Spirit + Speed == Tuning.StartingStatPoints) {
				return;
			}
		}
		Spirit = Spirit + 1;
		SpiritText.text = Spirit.ToString();
		ChangeStat.ChangePlayerMagic (Spirit);
		print (Spirit);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void SpiritMinusClick()
	{
		if (Spirit == 0) {
			return;}
		Spirit = Spirit - 1;
		SpiritText.text = Spirit.ToString();
		ChangeStat.ChangePlayerMagic (Spirit);
		print (Spirit);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void SpeedPlusClick()
	{
		if (IfCreation == 1) {
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
		ChangeStat.ChangePlayerSpeed (Speed);
		print (Speed);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
	public void SpeedMinusClick()
	{
		if (Speed == 0) {
			return;}
		Speed = Speed - 1;
		SpeedText.text = Speed.ToString();
		ChangeStat.ChangePlayerSpeed (Speed);
		print (Speed);
		print (Constitution + Skill + Strength + Spirit + Speed);
	}
}