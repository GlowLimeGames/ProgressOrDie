/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using UnityEngine;

public class CameraModule : Module 
{	
	Agent followAgent;
	bool isPaused;
	public void StartFollowing (Agent agent) {
		this.followAgent = agent;
	}

	public void PauseFollowing () {
		isPaused = true;
	}

	public void ResumeFollowing () {
		isPaused = false;
	}

	public void StopFollowing () {

	}

	void Update() {
		if (followAgent && !isPaused) {
			follow(this.followAgent);
		}
	}

	void follow(Agent followAgent) {
		Vector3 vec = followAgent.GetWorldPos();
		this.transform.position = new Vector3(vec.x, vec.y, transform.position.z);
	}

}
