using System;
using System.Collections.Generic;
using LitJson;

namespace Pomelo.DotNetClient
{
    public class EventManager : IDisposable
    {
        private Dictionary<uint, Action<JsonData>> callBackMap;
        private Dictionary<string, List<Action<string, JsonData>>> eventMap;

        public EventManager()
        {
            this.callBackMap = new Dictionary<uint, Action<JsonData>>();
            this.eventMap = new Dictionary<string, List<Action<string, JsonData>>>();
        }

        //Adds callback to callBackMap by id.
        public void AddCallBack(uint id, Action<JsonData> callback)
        {
            if (id > 0 && callback != null)
            {
                this.callBackMap[id] = callback;
            }
        }

        /// <summary>
        /// Invoke the callback when the server return messge .
        /// </summary>
        /// <param name='pomeloMessage'>
        /// Pomelo message.
        /// </param>
        public void InvokeCallBack(uint id, JsonData data)
        {
            if (!callBackMap.ContainsKey(id)) return;
            callBackMap[id].Invoke(data);
        }

        //Adds the event to eventMap by name.
        public void AddOnEvent(string eventName, Action<string, JsonData> callback)
        {
            List<Action<string, JsonData>> list = null;
            if (this.eventMap.TryGetValue(eventName, out list))
            {
                list.Add(callback);
            }
            else
            {
                list = new List<Action<string, JsonData>>();
                list.Add(callback);
                this.eventMap.Add(eventName, list);
            }
        }

        /// <summary>
        /// If the event exists,invoke the event when server return messge.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        ///
        public void InvokeOnEvent(string route, JsonData msg)
        {
            if (!this.eventMap.ContainsKey(route))
            {
                return;
            }

            List<Action<string, JsonData>> list = eventMap[route];
            foreach (Action<string, JsonData> action in list)
            {
                action.Invoke(route, msg);
            }
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected void Dispose(bool disposing)
        {
            this.callBackMap.Clear();
            this.eventMap.Clear();
        }
    }
}

