using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;
public class MixpanelScript : MonoBehaviour {

	/*/set this to the distinct ID /*/
	public string Token;
	public string DistinctID;


	// Use this for initialization
	public void Start () { 

//		Mixpanel.Token = Token;
		// Mixpanel.DistinctID = DistinctID;

		/*/ Set this to the distinct ID of the current user/*/


	


	

		// Mixpanel.Token= "1b6f1caa2d59c490c189621be1ea58e3";
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
				
			}

			public void OnGUI() {
				if(GUILayout.Button("Send Event"))
				{
//					Mixpanel.SendEvent(_eventName, new Dictionary<string, object>
//						{
//							{"property1", _property1},
//							{"property2", _property2},
//						});
				}
			}

}
