using UnityEngine.Events;
using UnityEngine;


namespace MSEProject.Assets.Scripts.Events
{

    public class StringEvent : UnityEvent<string>
    {
        void start()
        {
            Debug.Log("StringEvent");
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