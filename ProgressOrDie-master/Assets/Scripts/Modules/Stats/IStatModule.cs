/*
 * Authors: Ian Macpherson, Isaiah Mann
 * Description: An interface to handle unit stats logic
 */

public interface IStatModule {

	IPlayerLegend GetLegend(string legendId);

	bool PlayerEarnedLegend(ISaveModule saveModule);
	IPlayerLegend GetLegendEarned(ISaveModule saveModule);
	void GivePlayerLegend (ISaveModule saveModule);

}
