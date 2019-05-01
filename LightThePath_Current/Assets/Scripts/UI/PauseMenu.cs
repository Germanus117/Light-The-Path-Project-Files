using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsUI;
    public GameObject player;
    public GameObject playerCam;
    //public AudioClip BckgrdSong;
    public AudioSource MusicSound;
    public GameObject Music;

    // public PlayerController PC;


    void Start()
    {
        Music = GameObject.FindGameObjectWithTag("Music");
        MusicSound = Music.GetComponent<AudioSource>();

    }
    void Update()
    {
        //audioSource.clip = BckgrdSong;


        if (InputManager.pause)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerCam = GameObject.FindGameObjectWithTag("CamScript");
            Cursor.lockState = CursorLockMode.None;

            //MusicSound.Pause();

            //if (GameIsPaused)
            //{
            //    Resume();
            //}
            if (GameIsPaused)
            {
                Resume();
            }
            else if (!GameIsPaused)
            {
                Pause();
            }
        }

    }
    public void Resume()
    {
        Debug.Log("Resuming game...");
        pauseMenuUI.SetActive(false);
        //MusicSound.Play();
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponentInChildren<Weapon>().enabled = true;
        playerCam.GetComponent<CameraRig>().enabled = true;

    }
    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
        Cursor.visible = true;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        Debug.Log("pause function");
        pauseMenuUI.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Weapon>().enabled = false;
        playerCam.GetComponent<CameraRig>().enabled = false;
        GameIsPaused = true;
        Cursor.visible = true;
        //MusicSound.Pause();
    }
    public void Restart()
    {

        Debug.Log("Restarting Level....");

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //PC.SavePos = Vector3.zero;
        // PC.Saverot = new Quaternion(0, 0, 0, 0);
        GameIsPaused = false;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Weapon>().enabled = true;
        playerCam.GetComponent<CameraRig>().enabled = true;
        //MusicSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }
    public void MainMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene(0);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = true;
    }
    public void Back()
    {
        pauseMenuUI.SetActive(true);
        optionsUI.SetActive(false);
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }
}