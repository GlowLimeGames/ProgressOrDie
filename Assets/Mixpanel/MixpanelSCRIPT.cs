using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel; 
public class MixpanelSCRIPT : MonoBehaviour {

	/*/set this to the distinct ID /*/
	public string Token;
	public string DistinctID;


	// Use this for initialization
	public void Start () { 

		//		Mixpanel.Token = Token;
		// Mixpanel.DistinctID = DistinctID;

		/*/ Set this to the distinct ID of the current user/*/




		public string versionNumber;
		public static MixpanelController instance;


		Mixpanel.Token= "1b6f1caa2d59c490c189621be1ea58e3";
		Mixpanel.SuperProperties.Clear();
		// Mixpanel.DistinctID = "the distinct ID string of your user";
		/*//*/
		//		Mixpanel.Track();
		//
		//		Mixpanel.SendEvent("Restart Button");
		//		Mixpanel.SendEvent("Consumed Health Potion");
		// Mixpanel.SendEvent("Click Target", "{"monster", string "meelee/strength attack", "monster", string "ranged attack,);
		//		Mixpanel.SendEvent("Click Ability");
		// Mixpanel.SendEvent("Death",{"player", string "death", 


		//		Mixpanel.SuperProperties.Add("platform", Application.platform.ToString());
		//		Mixpanel.SuperProperties.Add("quality", QualitySettings.names[QualitySettings.GetQualityLevel()]);
		//		Mixpanel.SuperProperties.Add("fullscreen", Screen.fullScreen);
		//		Mixpanel.SuperProperties.Add("resolution", Screen.width + "x" + Screen.height);
		//


		/*/Timing Events /*/ 
		Mixpanel.StartTimedEvent("Meelee/Strength Attack Button");
		Mixpanel.StartTimedEvent("Ranged/Magic Button");
		Mixpanel.StartTimedEvent("Movement Overlay");
		Mixpanel.StartTimedEvent("Consumed Health Potions");
		Mixpanel.StartTimedEvent("Click Target");
		Mixpanel.StartTimedEvent("Click Ability");
		Mixpanel.StartTimedEvent("Allocate Stat points");
		Mixpanel.StartTimedEvent("Restart button");
		Mixpanel.StartTimedEvent("Arrows to increase stats");
		Mixpanel.StartTimedEvent ("Sphere of Influence");

	}

	//Tracks an event
	static void Track("Click Target");
	static void Track("Meelee/Strength Attack Button");
	static void Track("Arrows to increase stats");
	static void Track("Restart button");
	static void Track ("Arrows to increase stats");
	static void Track ("Click Ability");
	static void Track ("Movement Overlay");
	static void Track("Ranged/Magic Button");


	//Tracking 
	static void mixpanel.Mixpanel.Track(string  MonsterState, string Inactive, string Patrol, string Aggro);
	static void mixpanel.Mixpanel.Track(string MovementAcrossBoard, int speedpoints, int magicpoints, int strengthpoints, 
		int constitutionpoints, int skillpoints)

	//Loading level 
	void OnLevelWasLoaded (int level) {

	}

		

	//Completing tutorial
	public static void TutorialComplete (float time) {
		Mixpanel.SendEvent("Tutorial Complete", new Dictionary<string, object> {
			{"Completion Time", time}
		});
	}
	#endregion




		
	//public void OnGUI() {
		//if(GUILayout.Button("Send Event"))
	//	{
			//					Mixpanel.SendEvent(_eventName, new Dictionary<string, object>
			//						{
			//							{"property1", _property1},
			//							{"property2", _property2},
			//						});
	//	}
	//}






	//Report of Outcome of Combat for Players 
	public class BattleOutComeReport : Report {
		string achievementName;
		const string NEW_ACHIEVEMENT_NAME_KEY = "Victory";
		const string NEW_REPORT_NAME_KEY = "Death";


	public BattleOutComeReport (string achievementName) 
			this.achievementName = achievementName;
			Type = ReportType.Achievement;
		}
	



	/*/playtime report/*/

	public class PlaytimeReport : Report {

		bool hasFinishedTutorial;
		float playTime;
		Scenes currentScreen;

		const string HAS_FINISHED_TUTORIAL_KEY = "Finished Tutorial";
		const string PLAY_TIME_KEY = "Time Played";
		const string CURRENT_SCREEN_KEY = "Current Screen";

		public PlaytimeReport (bool hasFinishedTutorial, float playTime, Scenes currentScreen) {
			this.hasFinishedTutorial = hasFinishedTutorial;
			this.playTime = playTime;
			this.currentScreen = currentScreen;
			Type = ReportType.Playtime;
		}

		public override System.Collections.Generic.Dictionary<string, object> Generate () {
			return new System.Collections.Generic.Dictionary<string, object> () {
				{PLAY_TIME_KEY, MixpanelController.ConvertFloatToTimeString(this.playTime)},
				{HAS_FINISHED_TUTORIAL_KEY, this.hasFinishedTutorial ? "Yes" : "No"},
				{CURRENT_SCREEN_KEY, currentScreen.ToString()}
			};
		}

	}



	//tracking overall player progress: sent every time the player loads up crafting
	public static void Progress () {
		Mixpanel.SendEvent("Progress", new Dictionary<string, object> {
			{"Stat Points Earned", GlobalVars.STAT_POINTS_EARNEDED},
			{"Proceed to Next Level", Utility.ProceedToNextLevel()}
		});
	}



	void handleNamedEvents (string eventName) {
		switch (eventName) {

		case EventList.TUTORIAL_BUTTON_CLICKED:
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.MENU_BUTTON_CLICKED:
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.SOUND_TOGGLED_ON_OFF:
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.Meelee_Option;
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.Magic_Option
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.Ability_Option
			sendSimpleNamedEvent(eventName);
			break;

		case EventList.GAME_END_SCREEN_REACHED:
			sendSimpleNamedEvent(eventName);
			break;

		}
	}

	/// Clicking Buttons
	public static void RestartButton (){

		Mixpanel.SendEvent ("Tapped Restart Button", new Dictionary <string,object> ());
	}

	public static void FlipThroughMonstersWithArrows () {
		Mixpanel.SendEvent ("Flip Through Monsters With Arrows", new Dictionary <string,object> ());
	}
	public static void MenuButton (){
		Mixpanel.SendEvent("Tapped Menu Button", new Dictionary <string, object>())
	}



	/// Tapping on stuff
		public static void TappedOnMarketPlace () {
		Mixpanel.SendEvent ("Tapped on Market Place", new Dictionary <string,object> ());
	}
	public static void MoveButton (){
		Mixpanel.SendEvent ("Tapped on Move Button", new Dictionary <string, object> ()); 
	}


	public static void TappedOnMonster (){
			Mixpanel.SendEvent("Tapped on Monster", new Dictionary <string, object> ());
		}


	///Items After purchasing at Marketplace/Check Inventory

	void handleNamedTextEvents (string eventName, string text) {
		InventoryReport report;

		switch (eventName) {


		case EventList.INVENTORY_ITEM_COLLECTED:
			report = GameManager.InventoryManager.GetReport();
			report.AddItemCollected(text);
			sendInventoryEvent(eventName, report);
			break;

		case EventList.INVENTORY_ITEM_DESTROYED:
			report = GameManager.InventoryManager.GetReport();
			report.AddItemDestroyed(text);
			sendInventoryEvent(eventName, report);
			break;


		
	}
		// Send an inventory event (containing a full status on the inventory
		// TODO: Add an inventory report class: containing fall status of inventory in a dict
		void sendInventoryEvent (string eventName, InventoryReport inventoryReport) {
			Mixpanel.SendEvent (
				eventName,
				inventoryReport.Get()
			);
		}
			///when the player enters the tutorial 
	#region People Properties

	// Example people property
	// Common setup for the first use
	private static void FirstUse(string date, string distinct_id)
	{
		Mixpanel.SendPeople(new Dictionary<string ,object>{
			{"First Use", date},
			{"distinct_id", distinct_id},
		}, "set_once");
	}

	#endregion


	#region Super Properties
	//Add or Remove SuperProperties

	static public void AddSuperProperties(string propertyName, string propertyValue)
	{
		Mixpanel.SuperProperties.Add(propertyName, propertyValue);
	}

	static public void RemoveSuperProperties(string property)
	{
		Mixpanel.SuperProperties.Remove(property);
	}
	#endregion


	#region Helper Functions
	// Converts a float into a string that fits the format Minutes:Seconds:MilliSeconds
	public static string ConvertFloatToTimeString(float _time)
	{
		TimeSpan time = TimeSpan.FromSeconds(_time);
		return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Minutes, time.Seconds, time.Milliseconds);
	}
	#endregion
}

