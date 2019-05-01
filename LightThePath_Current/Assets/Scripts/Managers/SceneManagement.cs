using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : MonoBehaviour   
{
 
    [SerializeField]
    //private float delayBeforeLoading = 5f;  
    private float timeElapsed;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LevelTransition")
        {
            //this loads scene by game object name
            SceneManager.LoadScene(other.gameObject.name);
        }
        if (other.tag == "EndGame")
        {
            Invoke("Win", 5f);      
        }
    }

    void Win()
    {
        SceneManager.LoadScene("Credits Screen");
    }
}
