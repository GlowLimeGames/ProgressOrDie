/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

public class PlayerCharacter : Unit, IPlayerCharacter
{
	int speed;
	int magic;
	int constitution;
	int strength;
	int skill;
	int unspentStatPoints = 0;
	bool debuggingEnabled = false;
	public PlayerCharacter(UnitModule parent, MapLocation location, Map map, int startingStatPoints) : 
	base (parent, location, map) {
		setStatsToDefault();
		this.unspentStatPoints = startingStatPoints;
	}

	PlayerCharacterBehaviour player {
		get {
			return agent as PlayerCharacterBehaviour;
		}
	}

	void setStatsToDefault () {
		speed = 0;
		magic = 0;
		constitution = 0;
		strength = 0;
		skill = 0;
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
