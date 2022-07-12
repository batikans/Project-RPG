using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ProjectAssets.Project.Runtime.Core
{
    public class EventManager
    {
        private Dictionary<string, Action<EventParameters>> _eventDictionary;

        private static EventManager _eventManager;

        private static EventManager Instance
        {
            get
            {
                if (_eventManager == null)
                {
                    _eventManager = new EventManager();
                    _eventManager.Init();
                }

                return _eventManager;
            }
        }

        private void Init()
        {
            _eventDictionary ??= new Dictionary<string, Action<EventParameters>>();
        }

        public static void StartListening(string eventName, Action<EventParameters> listener)
        {
            if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                //Add more event to the existing one
                thisEvent += listener;

                //Update the Dictionary
                Instance._eventDictionary[eventName] = thisEvent;
            }
            else
            {
                //Add event to the Dictionary for the first time
                thisEvent += listener;
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action<EventParameters> listener)
        {
            if (_eventManager == null) return;
            if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                //Remove event from the existing one
                thisEvent -= listener;

                //Update the Dictionary
                Instance._eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, EventParameters eventParameters,
            [CallerMemberName] string memberName = "")
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;

            if (Instance._eventDictionary.TryGetValue(eventName, out var thisEvent))
            {
                thisEvent.Invoke(eventParameters);
                Debug.Log("**** EVENT **** " + eventName + " **** " + type?.Name + " **** " + memberName);
                // OR USE  instance.eventDictionary[eventName](eventParam);
            }
        }
    }

//Re-usable structure/ Can be a class to. Add all parameters you need inside it
    public struct EventParameters
    {
        public string StringParameter;
        public int INTParameter;
        public float FloatParameter;
        public bool IsTeleporting;
        public bool IsDead;
        public Vector3 PlayerSpawnPosition;
        public GameObject CharacterGameObject;
        public InputState InputState;
        public SceneName SceneName;
    }
}