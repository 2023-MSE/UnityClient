using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField] private GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickMap()
    {
        map.SetActive(true);
    }

    public void CloseMap()
    {
        map.SetActive(false);
    }
}
