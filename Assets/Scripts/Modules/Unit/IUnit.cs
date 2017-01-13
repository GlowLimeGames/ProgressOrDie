/*
 * Author: Isaiah Mann
 * Desc: An interface to define a Unit
 */

public interface IUnit {

	int GetSpeed();
	int GetConstitution();
	int GetMagic();
	int GetStrength();
	int GetSkill();

	MapLocation GetLocation();
	int X {get;}
	int Y {get;}

	AttackType[] GetAvailableAttacks();

	bool CanMoveTo (IMapTile tile);
	bool CanAttack (IUnit unit, AttackType attack);
	bool CanMeleeAttack (IUnit unit);
	bool CanMagicAttack (IUnit unit);

	void Damage (int damage);
	void MoveTo (IMapTile tile);
	void Attack(IUnit unit, AttackType attack);
	void MeleeAttack(IUnit unit);
	void MagicAttack(IUnit unit);
	void Kill ();

}
