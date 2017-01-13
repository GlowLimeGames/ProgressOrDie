/*
 * Author(s): Isaiah Mann
 * Description: A title the player can earn / be penalized with at the end of the round
 */

public interface IPlayerLegend {

	string GetId();

	// Current vals are Flee or Win (Death does not apply)
	GameEndType GetCause();

	// How many times you flee or win to earn this title
	int StreakCountToEarn();

	int GetStrengthModifier();
	int GetBulkModifier();
	int GetSoulModifier();

}
