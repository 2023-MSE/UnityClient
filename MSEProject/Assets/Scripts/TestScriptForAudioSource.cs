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
                        FindObjectOfType<NoteManager>().SetBpm(30f);
                        break;
                    case 1 :
                        FindObjectOfType<NoteManager>().SetBpm(45f);
                        break;
                    case 2 :
                        FindObjectOfType<NoteManager>().SetBpm(47f);
                        break;
                    case 3 :
                        FindObjectOfType<NoteManager>().SetBpm(56f);
                        break;
                    case 4 :
                        FindObjectOfType<NoteManager>().SetBpm(56f);
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
