/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class PlayerCharacter : Unit, IPlayerCharacter
{
	const string SPEED = "Speed";
	const string MAGIC = "Magic";
	const string CONST = "Constitution";
	const string STRENGTH = "Strength";
	const string SKILL = "Skill";
	const string UNSPENT_STAT_POINTS = "UnspentPoints";
	int speed
	{
		get 
		{
			return PlayerPrefs.GetInt(SPEED, 0);
		}
		set 
		{
			PlayerPrefs.SetInt(SPEED, value);
		}
	}

	int magic
	{
		get 
		{
			return PlayerPrefs.GetInt(SPEED, 0);
		}
		set 
		{
			PlayerPrefs.SetInt(SPEED, value);
		}
	}

	int constitution
	{
		get 
		{
			return PlayerPrefs.GetInt(SPEED, 0);
		}
		set 
		{
			PlayerPrefs.SetInt(SPEED, value);
		}
	}

	int strength
	{
		get 
		{
			return PlayerPrefs.GetInt(SPEED, 0);
		}
		set 
		{
			PlayerPrefs.SetInt(SPEED, value);
		}
	}

	int skill
	{
		get 
		{
			return PlayerPrefs.GetInt(SPEED, 0);
		}
		set 
		{
			PlayerPrefs.SetInt(SPEED, value);
		}
	}

	int unspentStatPoints {
		get {
			return PlayerPrefs.GetInt(UNSPENT_STAT_POINTS, getDefaultStatPoints());
		}
		set {
			PlayerPrefs.SetInt(UNSPENT_STAT_POINTS, value);
		}
	}

	bool debuggingEnabled = false;
	public PlayerCharacter(UnitModule parent, MapLocation location, Map map,
		int startingStatPoints, bool newCharacter) : 
	base (parent, location, map) {
		if(newCharacter) {
			setStatsToDefault();
		}
		this.unspentStatPoints = startingStatPoints;
	}

	PlayerCharacterBehaviour player {
		get {
			return agent as PlayerCharacterBehaviour;
		}
	}

	public override void Heal (float percent)
	{
		base.Heal (percent);
		if(HasAgentLink){
			agent.UpdateRemainingHealth(RemainingHealth);
		}
	}

	void setStatsToDefault () {
		speed = 0;
		magic = 0;
		constitution = 0;
		strength = 0;
		skill = 0;
		unspentStatPoints = getDefaultStatPoints();
	}
		
	public void PrintInfo(){
		printStats();
	}
		
	public bool HasUnspentStatPoints() {
		return unspentStatPoints > 0;
	}

	public int GetUnspentStatPoints() {
		return unspentStatPoints;
	}

	public bool TrySpendStatPoints(int amount) {
		if(amount <= unspentStatPoints) {
			unspentStatPoints -= amount;
			if(HasAgentLink) {
				player.UpdateStatPoints(unspentStatPoints);
			}
			return true;
		} else {
			return false;	
		}
	}

	int getDefaultStatPoints() {
		int defaultVal;
		if(TuningModule.Exists) {
			defaultVal = TuningModule.Get.StartingStatPoints;
		} else {
			defaultVal = default(int);
		}
		return defaultVal;
	}

	public void EarnStatPoints(int delta) {
		unspentStatPoints += delta;
		if(HasAgentLink) {
			player.UpdateStatPoints(unspentStatPoints);		
		}
	}

	void printStats(){
		if(debuggingEnabled) {
			UnityEngine.Debug.Log("Speed: " + speed);
			UnityEngine.Debug.Log("Magic: " + magic);
			UnityEngine.Debug.Log("Constitution: " + constitution);
			UnityEngine.Debug.Log("Strength: " + strength);
			UnityEngine.Debug.Log("Skill: " + skill);
		}
	}

	public string GetCritChanceAsPercentStr()
	{
		return parentModule.GetPlayerCritChanceAsPercentStr(this);
	}

	public float GetPlayerCritChanceAsPercentf()
	{
		return parentModule.GetPlayerCritChanceAsPercentf(this);
	}

	public override void Kill ()
	{
		base.Kill ();
		parentModule.HandlePlayerKilled();
		EventModule.Event("PlayerDeath");
	}

	public override AttackType GetPrimaryAttack()
	{
		return AttackType.Magic;
	}

	public override int GetSpeed () {
		return speed;
	}

	public override int GetMagic () {
		return magic;
	}

	public override int GetConstitution () {
		return constitution;
	}

	public override int GetStrength () {
		return strength;
	}

	public override int GetSkill () {
		return skill;
	}
		
	public override int ModSpeed(int delta) {
		speed += delta;
		printStats();
		return base.ModSpeed(delta);
	}

	public override int ModMagic (int delta) {
		magic += delta;
		printStats();
		return base.ModMagic(delta);
	}

	public override int ModConstitution(int delta) {
		constitution += delta;
		printStats();
		return base.ModConstitution(delta);
	}

	public override int ModStrength (int delta) {
		strength += delta;
		printStats();
		return base.ModStrength(delta);
	}

	public override int ModSkill (int delta) {
		skill += delta;
		printStats();
		return base.ModSkill(delta);
	}
}
