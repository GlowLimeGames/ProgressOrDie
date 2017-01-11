/*
 * Author(s): Xijie Guo, Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;
using System.Collections.Generic;

public class LegendModule : Module
{
	UnitModule units;
	LegendData legends;

	public void Init(StatModule stats, UnitModule units, LegendData legends) {
		this.units = units;
		this.legends = legends;
		print("The player has " + getPlayer().GetSpeed() + " speed");
	}

	void changePlayerSkill(int change) {
		units.ChangePlayerSkill(change);
	}

	void changePlayerStrength(int change) {
		units.ChangePlayerStrength(change);
	}
		
	void changePlayerMagic(int change) {
		units.ChangePlayerMagic(change);
	}
		
	void changePlayerSpeed(int change) {
		units.ChangePlayerSpeed(change);
	}
		
	void changePlayerConstitution(int change) {
		units.ChangePlayerConstitution(change);
	}

	int playerSkill() {
		return getPlayer().GetSkill();
	}

	int playerStrength() {
		return getPlayer().GetStrength();
	}

	int playerMagic () {
		return getPlayer().GetMagic();
	}

	int playerSpeed () {
		return getPlayer().GetSpeed();
	}

	int playerConstitution () {
		return getPlayer().GetConstitution();
	}

	int playerHealth () {
		return getPlayer().RemainingHealth;
	}

	PlayerCharacter getPlayer () {
		return units.GetMainPlayer().GetCharacter();
	}

	PlayerLegend getLegend(string legendType, int legendStreak) {
		return legends.Get(legendType, legendStreak);
	}

}

[System.Serializable]
public class LegendData : SerializableData
{
	public PlayerLegend[] Legends;
	Dictionary<string, Dictionary<int, PlayerLegend>> legendLookup;

	// Type can be Win or Flee. Count is how many times the player has done in a row
	public PlayerLegend Get(string type, int count) {
		checkLegends();
		Dictionary<int, PlayerLegend> gameType;
		if(legendLookup.TryGetValue(type, out gameType)) {
			PlayerLegend legend;
			if(gameType.TryGetValue(count, out legend)){
				return legend;
			} else {
				return getDefault();
			}
		} else {
			return getDefault();
		}
	}

	PlayerLegend getDefault() {
		return new PlayerLegend();	
	}

	void checkLegends() {
		if(legendLookup == null) {
			foreach (string gameEndKey in System.Enum.GetNames(typeof(GameEndType))) {
				legendLookup.Add(gameEndKey, new Dictionary<int, PlayerLegend>());
			}
			foreach (PlayerLegend legend in Legends) {

			}
		}
	}

}

[System.Serializable]
public class PlayerLegend : SerializableData
{
	public string Type;
	public int LevelCount;
	public string Legend;
	public int StrengthMod;
	public int ConstitutionMod;
	public int SkillMod;
	public int MagicMod;
}