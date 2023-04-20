using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private List<bool> skillAvailability;
    public override void hitMotion()
    {
        /*TODO*/
    }
    public override void dead()
    {
        /*TODO*/
        Debug.Log("Player dead");
        this.GetComponent<GameObject>().SetActive(false);
    }

    public void skillMotion()
    {

    }

    public void directionMotion(Direction direction)
    {
        /*TODO*/
    }
}
