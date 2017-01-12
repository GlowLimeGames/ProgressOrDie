/*
 * Author(s): Isaiah Mann
 * Description: [to be added]
 * Usage: [no notes]
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationModule : Module
{
	[SerializeField]
	float notificationLifetime;
	Text notificationText;
	string visibleNotifications {
		get {
			return _visibleNotifications;
		}
		set {
			_visibleNotifications = value;
			notificationText.text = value;
		}
	}
	string _visibleNotifications;
	Queue<string> notifications;
	bool runKillEvents = true;

	protected override void SetReferences () {
		base.SetReferences ();
		this.notificationText = GetComponentInChildren<Text>();
		notifications = new Queue<string>();
		runKillEvents = notificationLifetime > 0;
		StartCoroutine(killNotificationsOnTimer());
	}

	bool hasVisibleNotifications {
		get {
			return !string.IsNullOrEmpty(visibleNotifications);
		}
	}
		
	bool hasNotifications {
		get {
			return notifications.Count > 0;
		}
	}

	protected override void SubscribeEvents ()
	{
		base.SubscribeEvents ();
		EventModule.Subscribe(handlePODMessageEvent);
	}

	protected override void UnusbscribeEvents ()
	{
		base.UnusbscribeEvents ();
		EventModule.Unsubscribe(handlePODMessageEvent);
	}
		
	void receiveNotification(string notif) {
		notifications.Enqueue(notif);
		refreshNotificationDisplay(notifications);
	}

	void refreshNotificationDisplay(Queue<string> notifs){
		visibleNotifications = ArrayUtil.ToString(notifs.ToArray());
	}

	void handlePODMessageEvent(PODEvent gameEvent, string message) {
		if(isNotificationEvent(gameEvent)) {
			receiveNotification(message);
		}
	}

	bool isNotificationEvent(PODEvent gameEvent) {
		return gameEvent == PODEvent.Notification;
	}

	IEnumerator killNotificationsOnTimer() {
		// Effectively creates an infinite loop w/ a time delay
		bool hasSurvivedOnce = false;
		while(runKillEvents) {
			yield return new WaitForSecondsRealtime(notificationLifetime);
			if (hasNotifications) {
				if(notifications.Count > 1 || hasSurvivedOnce) {
					notifications.Dequeue();
					refreshNotificationDisplay(notifications);
					hasSurvivedOnce = false;
				} else if (notifications.Count == 1) {
					hasSurvivedOnce = true;
				}
			} 
		}
	}
}
