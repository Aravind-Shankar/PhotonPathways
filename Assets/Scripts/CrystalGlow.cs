using UnityEngine;

public class CrystalGlow : MonoBehaviour
{
    public ColliderCount1 script1;
    public ColliderCount2 script2;
    public ColliderCount3 script3;
    public ColliderCount4 script4;
    private bool crystalTriggered;
    public GameObject crystal; 

    void Update()
    {
        
        bool bool1 = script1.GetBool();
        bool bool2 = script2.GetBool();
        bool bool3 = script3.GetBool();
        bool bool4 = script4.GetBool();

        // make a decision based on the bool values
        if (bool1 && bool2 && bool3 && bool4)
        {
            crystal.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            crystalTriggered = true;
        }
        else
        {
            crystalTriggered = false;
        }
    }
}
