using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScriptForAudioSource : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public int index = -1;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "CombatScene")
        {
            index += 1;
            if (audioClips.Count > index)
            {
                audioSource.clip = audioClips[index];
                audioSource.Play();

                switch (index)
                {
                    case 0 :
                        FindObjectOfType<NoteManager>().bpm = 30F;
                        break;
                    case 1 :
                        FindObjectOfType<NoteManager>().bpm = 45F;
                        break;
                    case 2 :
                        FindObjectOfType<NoteManager>().bpm = 47F;
                        break;
                    case 3 :
                        FindObjectOfType<NoteManager>().bpm = 56F;
                        break;
                    case 4 :
                        FindObjectOfType<NoteManager>().bpm = 56F;
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
