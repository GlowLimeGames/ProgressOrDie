public class FleeModule : Module
{
	public const int BASE_FLEE = 10;

	PlayerCharacter player; 

	LegendModule legend;

	int currentConstitution;
	int currentSpeed;
	int currentMagic;
	int currentStrength;
	int currentSkill;

	int damageDealt = 0;

	int valueForFlee = 0;

	int CurrentConstitution() {
		return player.GetConstitution();
	}

	int CurrentSpeed() {
		return player.GetSpeed();
	}

	int CurrentSkill() {
		return player.GetSkill();
	}

	int CurrentMagic() {
		return player.GetMagic();
	}

	int CurrentStrength() {
		return player.GetStrength();
	}

	int CalculateFlee() {
		if(currentConstitution <= damageDealt) {
			currentConstitution = currentConstitution - damageDealt;
			valueForFlee = currentConstitution + BASE_FLEE;
		}
		return valueForFlee;
	}


	public bool CanSuccessfullyFlee() {
		if(CalculateFlee() > 0) {
			return true;
		}
		return false;
	}

	public void ResetStats(string type, int count) {
		if(CanSuccessfullyFlee()) {
			legend.changeStats(type, count);

		}
	}

}

