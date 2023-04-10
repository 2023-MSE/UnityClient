using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    private AudioSource myAudio;

    private bool AudioCheck;
    // Start is called before the first frame update
    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        AudioCheck = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (AudioCheck == false)
        {
            if (col.CompareTag("Note"))
            {
                myAudio.Play();
                AudioCheck = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
