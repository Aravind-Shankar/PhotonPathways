using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCount1 : MonoBehaviour
{
    private bool myBool;

    void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.tag == "Player")
            myBool = true;
    }

    public bool GetBool()
    {
        return myBool;
    }
}