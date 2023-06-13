using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private GameObject imgae;

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
        imgae.SetActive(false);
    }

    public void gameover()
    {
        imgae.SetActive(true);
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

   


    public void fadein_map(ulong i)
    {
        check = true;
        Debug.Log("fadeinstart");
        if (fadeInOnStart)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn(i));
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


    IEnumerator FadeIn(ulong i)
    {
        imgae.SetActive(true);
        while (canvasGroup.alpha < 1)
        {
            Debug.Log("fade in : " +canvasGroup.alpha);
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;

            yield return null;
        }
        if (fadeOutOnExit)
        {
            nextcheck = true;
            StartCoroutine(FadeOut(i));
            
        }

        yield return null;
        imgae.SetActive(false);
    }

    IEnumerator FadeOut(ulong i)
    {
        StartCoroutine(nextstage(1.5f,i));
        while (canvasGroup.alpha > 0)
        {
            Debug.Log("fade out : " +canvasGroup.alpha);
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;

           
            yield return null;
        }
        
        check = false;
    }

    IEnumerator nextstage(float wait,ulong i)
    {
        yield return wait;
        //_Player.CombatScene.DungeonManager.instance.GetNextStages();
        
        
        
       _Player.CombatScene.DungeonManager.Instance.GoNextStage(
           // _Player.CombatScene.DungeonManager.Instance.GetCurrentStage() + 1
           i
        );
        
    }

}
