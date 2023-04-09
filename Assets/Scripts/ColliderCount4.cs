using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCount4 : MonoBehaviour
{
    private bool myBool;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            myBool = true;
    }

    public bool GetBool()
    {
        return myBool;
    }
}