using UnityEngine.Events;
using UnityEngine;
using System;

namespace MSEProject.Assets.Scripts.Events
{
    [Serializable]
    public class StringEvent : UnityEvent<string>
    {
        void start()
        {
            Debug.Log("StringEvent");
        }
    }
    [Serializable]
    public class IntEvent : UnityEvent<int>
    {
        void start()
        {
            Debug.Log("intEvent");
        }
    }
    [Serializable]
    public class DirEvent : UnityEvent<Direction>
    {

        void start()
        {
            Debug.Log("dirEvent");
        }
    }

    public class SuccessAttackUnityEvent :UnityEvent
    {
        void start()
        {
            Debug.Log("SuccessAttackUnityEvent");
        } 
    }
    public class FailAttackUnityEvent :UnityEvent
    {
        void start()
        {
            Debug.Log("FailAttackUnityEvent");
        }
    }
    public class SuccessGenalizeUnityEvent :UnityEvent<string>
    {
        void start()
        {
            Debug.Log("SuccessGenalizeUnityEvent");
        }
    }
    public class FailGenalizeUnityEvent :UnityEvent
    {
        void start()
        {
            Debug.Log("FailGenalizeUnityEvent");
        }
    }
}