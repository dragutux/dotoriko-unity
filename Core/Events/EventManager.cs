using System;
using System.Collections.Generic;

/**
 * Event System
 * Gives ability to subscribe on events, and to trigger them later
 * All user defined events must impletement IEvent interface 
 * 
 * @namespace DotOriko.Core.EventSystem
 * @from Sharpy (https://github.com/Inlife/sharpy)
 * @author Vladislav Gritsenko (Inlife)
 * @year 2015
 * @licence MIT
 */
namespace DotOriko.Core.Events {

    /**
     * Event Manager
     * You can subscribe to events and trigger them
     * @depends System.Action, System.Collections.Generic.Dictionary
     */
    public class EventManager : DotOrikoSingleton<EventManager> {

        /**
         * Stores all information about event and handlers
         */
        private Dictionary<string, Dictionary<int, List<Action<object[]>>>> __register;

        /**
         * Creates Manager entity
         * @constructor
         */
        public EventManager() {
            this.__register = new Dictionary<string, Dictionary<int, List<Action<object[]>>>>();
        }

        /**
         * Register callback by object relation (for later to safely delete)
         */
        public int On(UnityEngine.Object localObject, string eventName, Action<object[]> callback) {
            return this.On(localObject.GetInstanceID(), eventName, callback);
        }

        /**
         * Regiser an event handlers, that will be listening events by "eventName" to default id = 0 
         */
        public int On(string eventName, Action<object[]> callback) {
            return this.On(0, eventName, callback);
        }

        /**
         * Regiser an event handlers, that will be listening events by "eventName" by manual id
         */
        public int On(int id, string eventName, Action<object[]> callback) {
            // check for existance by eventName
            if (!this.__register.ContainsKey(eventName)) {
                this.__register[eventName] = new Dictionary<int, List<Action<object[]>>>();
            }

            // check for existance by object id
            if (!this.__register[eventName].ContainsKey(id)) {
                this.__register[eventName][id] = new List<Action<object[]>>();
            }

            // add callback to list of actions
            this.__register[eventName][id].Add(callback);
            return id;
        }

        /**
         * Alias for "On" method
         */
        public int Register(UnityEngine.Object localObject, string eventName, Action<object[]> callback) {
            return this.On(localObject, eventName, callback);
        }

        /**
         * Alias for "On" method
         */
        public int Register(string eventName, Action<object[]> callback) {
            return this.On(eventName, callback);
        }

        /**
         * Alias for "On" method
         */
        public int Register(int id, string eventName, Action<object[]> callback) {
            return this.On(id, eventName, callback);
        }

        /**
         * Remove all event handlers attached to specified event
         */
        public void RemoveForEvent(string eventName) {
            if (this.__register.ContainsKey(eventName)) {
                this.__register.Remove(eventName);
            }
        }

        /**
         * Remove all event handlers related to specified id
         */
        public void RemoveForId(int id) {
            foreach (var eventMember in this.__register) {
                this.RemoveForEventById(eventMember.Key, id);
            }
        }

        /**
         * Remove all event handlers related to specified object
         */
        public void RemoveForObject(UnityEngine.Object localObject) {
            this.RemoveForId(localObject.GetInstanceID());
        }

        /**
         * Remove all event handlers attached to specified event and
         * that are registered by custom id
         */
        public void RemoveForEventById(string eventName, int id) {
            if (this.__register.ContainsKey(eventName) && this.__register[eventName].ContainsKey(id)) {
                this.__register[eventName].Remove(id);
            }
        }

        /**
         * Remove all event handlers attached to specified event and
         * that are registered to specified Object
         */
        public void RemoveForEventByObject(string eventName, UnityEngine.Object localObject) {
            this.RemoveForEventById(eventName, localObject.GetInstanceID());
        }
        
        /**
         * Triggers the event, by it's eventName, default event object will be passed to callback
         */
        public void Trigger(string eventName) {
            this.Trigger(eventName, new DefaultEvent());
        }

        /**
         * Triggers the event by it's name, "data" event object will be passed to callback
         */
        public void Trigger(string eventName, params object[] data) {
            if (!this.__register.ContainsKey(eventName)) return;

            foreach (KeyValuePair<int, List<Action<object[]>>> member in this.__register[eventName]) {
                foreach (Action<object[]> callback in member.Value) {
                    callback(data);
                }
            }

            return;
        }

        /**
         * Depreacted!!
         */
        ///<deprecated>RemoveEvent</deprecated>
        public void RemoveEvent(string eventName) {
            this.RemoveForEvent(eventName);
        }
    }
}
