/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

[System.Serializable]
public class AbilityData 
{
	public PlayerAbility[] Abilities;
}

[System.Serializable]
public class PlayerAbility
{
	public string Ability;
	public int StrengthRequirement;
	public int SoulRequirement;
	public int AgilityRequirement;
	public int BulkRequirement;
	public int PrecisionRequirement;
	public int TraitToll;

}
