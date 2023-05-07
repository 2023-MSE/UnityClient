using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoHolder : Singleton<UserInfoHolder>
{
    public UserInfo myInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
