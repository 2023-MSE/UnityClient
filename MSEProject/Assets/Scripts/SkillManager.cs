using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<GameObject> effects = new List<GameObject>();
    public List<GameObject> skills = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject i in effects)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in skills)
        {
            i.SetActive(false);
        }
        
    }

    // Update is called once per frame
    public void Skill_Input()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skills[0].SetActive(true);
            
            skills[1].SetActive(false);
            skills[2].SetActive(false);
            
            effects[0].SetActive(true);
            
            effects[1].SetActive(false);
            effects[2].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skills[1].SetActive(true);
            
            skills[0].SetActive(false);
            skills[2].SetActive(false);
            
            effects[1].SetActive(true);
            
            effects[0].SetActive(false);
            effects[2].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skills[2].SetActive(true);
            
            skills[0].SetActive(false);
            skills[1].SetActive(false);
            
            effects[2].SetActive(true);
            
            effects[0].SetActive(false);
            effects[1].SetActive(false);
        }
    }
    
    
}
