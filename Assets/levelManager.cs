using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public GameObject[] reflectors;
    public GameObject crystal;
    private AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        int counter = 0;
        for(int i = 0; i < reflectors.Length; i++)
        {
            Reflector reflector = reflectors[i].transform.GetChild(0).GetComponent<Reflector>();
            if(reflector.isCounted == true)
            {
                counter++;
            }
        }

        if(counter >= 4)
        {
            Debug.Log(counter);
            crystal.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            audioData.Play(0);
        }
    }
}
