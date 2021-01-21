using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearScript : MonoBehaviour
{
    //script for animate settings gear
   public void TurnGear()
    {
        if (gameObject.GetComponent<Animator>().GetBool("BTNisPressed"))
        {
            gameObject.GetComponent<Animator>().SetBool("BTNisPressed", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("BTNisPressed", true);
        }
    }
}
