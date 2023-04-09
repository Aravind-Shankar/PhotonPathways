using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "uiCustom") {
            Debug.Log("HITT!!");
            Invoke("action", 2.0f);
            gameObject.SetActive(false);
            
        }
    }

    public void action() {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Aravind2");
    }
}
