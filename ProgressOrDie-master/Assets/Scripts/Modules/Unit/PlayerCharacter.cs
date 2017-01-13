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

	public PlayerCharacter(UnitModule parent, MapLocation location, Map map) : 
	base (parent, location, map) {
		setStatsToDefault();
	}

	void setStatsToDefault () {
		speed = 9;
		magic = 9;
		constitution = 9;
		strength = 9;
		skill = 9;
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
		return base.ModSpeed(delta);
	}

	public override int ModMagic (int delta) {
		magic += delta;
		return base.ModMagic(delta);
	}

	public override int ModConstitution(int delta) {
		constitution += delta;
		return base.ModConstitution(delta);
	}

	public override int ModStrength (int delta) {
		strength += delta;
		return base.ModStrength(delta);
	}

	public override int ModSkill (int delta) {
		skill += delta;
		return base.ModSkill(delta);
	}
}
