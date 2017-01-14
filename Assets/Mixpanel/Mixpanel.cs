using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using UnityEngine;

public static class Mixpanel
{
	// Set this to your Mixpanel token.
	public static string Token;

	// Set this to the distinct ID of the current user.
	public static string DistinctID;

	// Set to true to enable debug logging.
	public static bool EnableLogging;

	// Add any custom "super properties" to this dictionary. These are properties sent with every event.
	public static Dictionary<string, object> SuperProperties = new Dictionary<string, object>();

	private const string API_URL_FORMAT = "http://api.mixpanel.com/track/?data={0}";
	private static MonoBehaviour _coroutineObject;

// Call this to send an event to Mixpanel.
	// eventName: The name of the event. (Can be anything you'd like.)
	public static void SendEvent(string eventName)
	{
		SendEvent(eventName, null);
	}
        /// <summary>
        /// Clears all current event timers.
        /// </summary>
        public static void ClearTimedEvents()
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                instance.clear_timed_events();
            #endif
        }

        /// <summary>
        /// Clears the event timer for a single event.
        /// </summary>
        /// <param name="eventName">the name of event to clear event timer</param>
        public static bool ClearTimedEvent(string eventName)
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                return instance.clear_timed_event(eventName);
            #endif
            return false;
        }

        /// <summary>
        ///  Uploads queued data to the %Mixpanel server.
        /// </summary>
        public static void FlushQueue()
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                instance.flush_queue();
            #endif
        }

        /// <summary>
        /// Registers super properties, overwriting ones that have already been set.
        /// </summary>
        /// <param name="key">name of the property to register</param>
        /// <param name="value">value of the property to register</param>
        public static void Register(string key, Value value) {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                instance.register_(key, value);
            #endif
        }

        /// <summary>
        /// Registers super properties without overwriting ones that have already been set.
        /// </summary>
        /// <param name="key">name of the property to register</param>
        /// <param name="value">value of the property to register</param>
        public static bool RegisterOnce(string key, Value value) {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                return instance.register_once(key, value);
            #endif
            return false;
        }

        /// <summary>
        /// Clears all distinct_ids, superProperties, and push registrations from persistent storage.
        /// Will not clear referrer information.
        /// </summary>
        public static void Reset()
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                instance.reset();
            #endif
        }

        /// <summary>
        /// Start timing of an event. Calling Mixpanel.StartTimedEvent(string eventName) will not send an event,
        /// but when you eventually call Mixpanel.Track(string eventName), your tracked event will be sent with a "$duration" property,
        /// representing the number of seconds between your calls.
        /// </summary>
        /// <param name="eventName">the name of the event to track with timing</param>
HEAD
		public static bool StartTimedEvent(string eventName)
        
           #if !DISABLE_MIXPANEL
		{ if (tracking_enabled)
		return instance.start_timed_event("eventName");
            #endif
		return false;
	}
	

        public static bool StartTimedEvent(string eventName)
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                return instance.start_timed_event(eventName);
            #endif
            return false;
        }
origin/analytic

        /// <summary>
        /// Begin timing of an event, but only if the event has not already been registered as a timed event.
        /// Useful if you want to know the duration from the point in time the event was first registered.
        /// </summary>
        /// <param name="eventName">the name of the event to track with timing</param>
        public static bool StartTimedEventOnce(string eventName)
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                return instance.start_timed_event_once(eventName);
            #endif
            return false;
        }

        /// <summary>
        /// Tracks an event.
        /// </summary>
        /// <param name="eventName">the name of the event to send</param>
        public static void Track(string eventName)
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                instance.track(eventName, new Value());
            #endif
        }

        /// <summary>
        /// Tracks an event with properties.
        /// </summary>
        /// <param name="eventName">the name of the event to send</param>
        /// <param name="properties">A JSONObject containing the key value pairs of the properties
        /// to include in this event. Pass null if no extra properties exist.
        /// </param>
        public static void Track(string eventName, Value properties)
        {
            #if !DISABLE_MIXPANEL
            if (tracking_enabled)
                instance.track(eventName, properties);
            #endif
        }

        /// <summary>
        /// Removes a single superProperty.
        /// </summary>
        /// <param name="key">name of the property to unregister</param>
        public static bool Unregister(string key) {
            if (tracking_enabled)
                return instance.unregister(key);
            return false;
        }

        /// <summary>
        /// Core interface for using %Mixpanel %People Analytics features. You can get an instance by calling Mixpanel.people
        /// </summary>
        public class People
        {

            /// <summary>
            /// Append values to list properties.
            /// </summary>
            /// <param name="properties">mapping of list property names to values to append</param>
            public void Append(Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.append_properties(properties);
                #endif
            }

            /// <summary>
            /// Appends a value to a list-valued property.
            /// </summary>
            /// <param name="listName">the %People Analytics property that should have it's value appended to</param>
            /// <param name="value">the new value that will appear at the end of the property's list</param>
            public void Append(string listName,  Value value)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.append(listName, value);
                #endif
            }

            /// <summary>
            /// Permanently clear the whole transaction history for the identified people profile.
            /// </summary>
            public void ClearCharges()
            {
                if (tracking_enabled)
                    mixpanel.people.clear_charges();
            }

            /// <summary>
            /// Change the existing values of multiple %People Analytics properties at once.
            /// </summary>
            /// <param name="properties"> A map of String properties names to Long amounts. Each property associated with a name in the map </param>
            public void Increment(Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.increment_properties(properties);
                #endif
            }

            /// <summary>
            /// Convenience method for incrementing a single numeric property by the specified amount.
            /// </summary>
            /// <param name="property">property name</param>
            /// <param name="by">amount to increment by</param>
            public void Increment(string property,  Value by)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.increment(property, by);
                #endif
            }


            /// <summary>
            /// Set a collection of properties on the identified user all at once.
            /// </summary>
            /// <param name="properties">a JSONObject containing the collection of properties you wish to apply
            /// to the identified user. Each key in the JSONObject will be associated with a property name, and the value
            /// of that key will be assigned to the property.
            /// </param>
            public void Set(Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.set_properties(properties);
                #endif
            }

            /// <summary>
            /// Sets a single property with the given name and value for this user.
            /// </summary>
            /// <param name="property">property name</param>
            /// <param name="to">property value</param>
            public void Set(string property,  Value to)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.set(property, (detail.Value)to);
                #endif
            }

            /// <summary>
            /// Like Mixpanel.Set(string property, Value to), but will not set properties that already exist on a record.
            /// </summary>
            /// <param name="property">property name</param>
            /// <param name="to">property value</param>
            public void SetOnce(string property,  Value to)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.set_once(property, to);
                #endif
            }

            /// <summary>
            /// Like Mixpanel.Set(Value properties), but will not set properties that already exist on a record.
            /// </summary>
            /// <param name="properties">a JSONObject containing the collection of properties you wish to apply to the identified user. Each key in the JSONObject will be associated with a property name, and the value of that key will be assigned to the property.</param>
            public void SetOnce(Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.set_once_properties(properties);
                #endif
            }

            /// <summary>
            /// Track a revenue transaction for the identified people profile.
            /// </summary>
            /// <param name="amount">amount of revenue received</param>
            public void TrackCharge(double amount)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.track_charge(amount, new Value());
                #endif
            }

            /// <summary>
            /// Adds values to a list-valued property only if they are not already present in the list.
            /// </summary>
            /// <param name="listName">name of the list-valued property to set or modify</param>
            /// <param name="values">an array of values to add to the property value if not already present</param>
            public void TrackCharge(double amount, Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.track_charge(amount, properties);
                #endif
            }

            /// <summary>
            /// Adds values to a list-valued property only if they are not already present in the list.
            /// If the property does not currently exist, it will be created with the given list as it's value.
            /// If the property exists and is not list-valued, the union will be ignored.
            /// </summary>
            /// <param name="properties">mapping of list property names to lists to union</param>
            public void Union(Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.union_properties(properties);
                #endif
            }

            /// <summary>
            /// Adds values to a list-valued property only if they are not already present in the list.
            /// If the property does not currently exist, it will be created with the given list as it's value.
            /// If the property exists and is not list-valued, the union will be ignored.            /// </summary>
            /// <param name="listName">name of the list-valued property to set or modify</param>
            /// <param name="values">an array of values to add to the property value if not already present</param>
            public void Union(string listName,  Value values)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.union_(listName, values);
                #endif
            }

            /// <summary>
            /// Remove a list of properties and their values from the current user's profile in %Mixpanel %People.
            /// </summary>
            /// <param name="properties">properties array</param>
            public void Unset(Value properties)
            {
                #if !DISABLE_MIXPANEL
                if (tracking_enabled)
                    mixpanel.people.unset_properties(properties);
                #endif
            }
Stashed changes

	// Call this to send an event to Mixpanel.
	// eventName: The name of the event. (Can be anything you'd like.)
	// properties: A dictionary containing any properties in addition to those in the Mixpanel.SuperProperties dictionary.
	public static void SendEvent(string eventName, IDictionary<string, object> properties)
	{
		if(string.IsNullOrEmpty(Token))
		{
			Debug.LogError("Attempted to send an event without setting the Mixpanel.Token variable.");
			return;
		}
		
		if(string.IsNullOrEmpty(DistinctID))
		{
			if(!PlayerPrefs.HasKey("mixpanel_distinct_id"))
				PlayerPrefs.SetString("mixpanel_distinct_id", Guid.NewGuid().ToString());
			DistinctID = PlayerPrefs.GetString("mixpanel_distinct_id");
		}

		Dictionary<string, object> propsDict = new Dictionary<string, object>();
		propsDict.Add("distinct_id", DistinctID);
		propsDict.Add("token", Token);
		foreach(var kvp in SuperProperties)
		{
			if(kvp.Value is float) // LitJSON doesn't support floats.
			{
				float f = (float)kvp.Value;
				double d = f;
				propsDict.Add(kvp.Key, d);
			}
			else
			{
				propsDict.Add(kvp.Key, kvp.Value);
			}
		}
		if(properties != null)
		{
			foreach(var kvp in properties)
			{
				if(kvp.Value is float) // LitJSON doesn't support floats.
				{
					float f = (float)kvp.Value;
					double d = f;
					propsDict.Add(kvp.Key, d);
				}
				else
				{
					propsDict.Add(kvp.Key, kvp.Value);
				}
			}
		}
		Dictionary<string, object> jsonDict = new Dictionary<string, object>();
		jsonDict.Add("event", eventName);
		jsonDict.Add("properties", propsDict);
		string jsonStr = JsonMapper.ToJson(jsonDict);
		if(EnableLogging)
			Debug.Log("Sending mixpanel event: " + jsonStr);
		string jsonStr64 = EncodeTo64(jsonStr);
		string url = string.Format(API_URL_FORMAT, jsonStr64);
		StartCoroutine(SendEventCoroutine(url));
	}

	private static string EncodeTo64(string toEncode)
	{
		var toEncodeAsBytes = Encoding.ASCII.GetBytes(toEncode);
		var returnValue = Convert.ToBase64String(toEncodeAsBytes);
		return returnValue;
	}

	private static void StartCoroutine(IEnumerator coroutine)
	{
		if(_coroutineObject == null)
		{
			var go = new GameObject("Mixpanel Coroutines");
			UnityEngine.Object.DontDestroyOnLoad(go);
			_coroutineObject = go.AddComponent<MonoBehaviour>();
		}

		_coroutineObject.StartCoroutine(coroutine);
	}

	private static IEnumerator SendEventCoroutine(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		if(www.error != null)
			Debug.LogWarning("Error sending mixpanel event: " + www.error);
		else if(www.text.Trim() == "0")
			Debug.LogWarning("Error on mixpanel processing event: " + www.text);
		else if(EnableLogging)
			Debug.Log("Mixpanel processed event: " + www.text);
	
	}