using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEventScriptTest : MonoBehaviour
{

    public GameObject RedCenter;

    public GameObject BlueCenter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void check_fail()
    {
        StartCoroutine(redColor());
    }
    
    public void check_success()
    {
        StartCoroutine(BlueColor());
    }
    
    IEnumerator redColor()
    {
        RedCenter.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RedCenter.SetActive(false);
    }
    IEnumerator BlueColor()
    {
        BlueCenter.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        BlueCenter.SetActive(false);
    }
}
