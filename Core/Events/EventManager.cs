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
         * @property
         */
        private Dictionary<string, List<Action<object[]>>> __register;

        /**
         * Creates Manager entity
         * @constructor
         */
        public EventManager() {
            this.__register = new Dictionary<string, List<Action<object[]>>>();
        }

        /**
         * Regiser an event handlers, that will be listening events by "name"
         * 
         * @param {string} name - Name of event to register
         * @param {Action<Event>} callback - Callback anonymous function 
         * @return {void}
         */
        public int On(string name, Action<object[]> callback) {
            if (!this.__register.ContainsKey(name)) {
                this.__register[name] = new List<Action<object[]>>();
            }
            this.__register[name].Add(callback);
            return this.__register[name].IndexOf(callback);
        }

        public void RemoveEvent(string name) {
            this.__register.Remove(name);
        }

        public void RemoveHandler(int handler, string evnt) {
            this.__register[evnt].RemoveAt(handler);
            if(this.__register[evnt].Count <= 0) {
                this.__register.Remove(evnt);
            }
        }
        
        /**
         * Triggers the event, by it's name, default event object will be passed to callback
         * 
         * @param {string} name - Name of event to trigger
         * @return {void}
         */
        public void Trigger(string name) {
            this.Trigger(name, new DefaultEvent());
        }

        /**
         * Triggers the event by it's name, "data" event object will be passed to callback
         * 
         * @param {string} name - Name of event to trigger
         * @param {Event} e - Event that will be passed to handler
         * @return {void}
         */
        public void Trigger(string name, params object[] data) {
            if (this.__register.ContainsKey(name)) {
                foreach (Action<object[]> callback in this.__register[name]) {
                    callback(data);
                }
            }
        }

    }

}
