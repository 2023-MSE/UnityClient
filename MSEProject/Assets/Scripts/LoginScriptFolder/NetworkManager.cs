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
        /*TODO : login request 를 처리함과 동시에 Dungeon Deploy 시 사용할 수 있는 User 정보를 받아와 저장해두어야 함. (Deploy할 때 해당 유저의 던전 목록에 접근할 수 있도록.)*/
        return SigninupResult.INVALID_ID;
    }
    
    public SigninupResult requestSignup(string ID, string password, string name)
    {
        /*TODO*/
        return SigninupResult.INVALID_ID;
    }
}
