using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExample : MonoBehaviour
{

    public UnityEvent<SigninupResult> successSkillNote;

    // Start is called before the first frame update
    void Start()
    {
        successSkillNote.Invoke(SigninupResult.INVALID_ID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
