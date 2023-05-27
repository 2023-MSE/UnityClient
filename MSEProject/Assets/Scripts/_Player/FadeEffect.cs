using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{

    public float fadeSpeed = 0.2f;
    public bool fadeInOnStart = true;
    public bool fadeOutOnExit = true;


    private DungeonManager _dungeonManager;

    private bool check=false;
    private bool nextcheck = false;
    
    
    private CanvasGroup canvasGroup;

    private TestDirButton test;
    public void Start()
    {
        test = FindObjectOfType<TestDirButton>();
        canvasGroup = gameObject.GetComponentInChildren<CanvasGroup>();
        _dungeonManager = FindObjectOfType<DungeonManager>();
    }

    public void gameover()
    {
        StartCoroutine(Over());
        
        
    }
    IEnumerator Over()
    {
        while (canvasGroup.alpha < 1)
        {
            Debug.Log("fade in : " +canvasGroup.alpha);
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;

            yield return null;
        }

        Time.timeScale = 0;


    }

   

    public void fadein()
    {
        check = true;
        Debug.Log("fadeinstart");
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn());
        }
    }

 

    public bool isFading()
    {
        return check;
    }

    public bool gotonext()
    {
        return nextcheck;
    }


    IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            Debug.Log("fade in : " +canvasGroup.alpha);
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;

            yield return null;
        }
        if (fadeOutOnExit)
        {
            nextcheck = true;
            StartCoroutine(FadeOut());
            
        }

       
    }

    IEnumerator FadeOut()
    {
        StartCoroutine(nextstage(1.5f));
        while (canvasGroup.alpha > 0)
        {
            Debug.Log("fade out : " +canvasGroup.alpha);
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;

           
            yield return null;
        }
        
        check = false;
       
    }

    IEnumerator nextstage(float wait)
    {
        yield return wait;
        _Player.CombatScene.DungeonManager.instance.GetNextStages();
        
        
        
        _Player.CombatScene.DungeonManager.Instance.GoNextStage(
            _Player.CombatScene.DungeonManager.Instance.GetCurrentStage() + 1
        );
    }

}
