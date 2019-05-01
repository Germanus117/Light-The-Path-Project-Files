using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IntroController : MonoBehaviour {


    public GameObject introductionUI;
    public static bool reading;
    public GameObject player;
    public GameObject playerCam;

    public void Awake()
    {
        showMessage();
    }

    private void Update()
    {
        if (reading == true)
        {
            showMessage();
        }
        if (reading == false)
        {
            hideMessage();
        }
    }

    public void showMessage()
    {
        reading = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Weapon>().enabled = false;
        playerCam.GetComponent<CameraRig>().enabled = false;

        Cursor.visible = true;

        introductionUI.SetActive(true);
    }

    public void hideMessage()
    {
        reading = false;

        Time.timeScale = 1f;

        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Weapon>().enabled = true;
        playerCam.GetComponent<CameraRig>().enabled = true;

        Cursor.visible = false;

        introductionUI.SetActive(false);
    }
}
