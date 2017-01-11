/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine.UI;

public class EnemyNPC : Unit, IEnemyNPC
{
	public EnemyDescriptor Descriptor{get; private set;}

	public EnemyNPC(UnitModule parent, EnemyDescriptor descriptor, MapLocation location, Map map) :
	base (parent, location, map) {
		this.Descriptor = descriptor;
	}

	public override int GetSpeed () {
		return Descriptor.Speed;
	}

	public override int GetConstitution () {
		return Descriptor.Constitution;
	}

	public override  int GetSkill () {
		return Descriptor.Skill;
	}

	public override int GetStrength () {
		return Descriptor.Strength;
	}

	public override int GetMagic () {
		return Descriptor.Magic;
	}

	public override int ModSpeed(int delta) {
		Descriptor.Speed += delta;
		return base.ModSpeed(delta);
	}

	public override int ModMagic (int delta) {
		Descriptor.Magic += delta;
		return base.ModMagic(delta);
	}
		
	public override int ModConstitution(int delta) {
		Descriptor.Constitution += delta;
		return base.ModConstitution(delta);
	}

	public override int ModStrength (int delta) {
		Descriptor.Strength += delta;
		return base.ModStrength(delta);
	}

	public override int ModSkill (int delta) {
		Descriptor.Skill += delta;
		return base.ModSkill(delta);
	}

}
