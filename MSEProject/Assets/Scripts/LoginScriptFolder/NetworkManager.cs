using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance = null;

    private NetworkManager() { }
    public static NetworkManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public SigninupResult requestSignin(string ID, string password)
    {
        /*TODO login requset*/
        return SigninupResult.INVALID_ID;
    }
    
    public SigninupResult requestSignup(string ID, string password, string name)
    {
        /*TODO*/
        return SigninupResult.INVALID_ID;
    }
}
