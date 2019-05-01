using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject MusicCreditsUI;

    public Vector3 SavePos;
    public Quaternion Saverot;

    public void Awake()
    {
        Cursor.visible = true;
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;

    }
    public void PlayGame()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(false);
        Cursor.visible = false;
        SceneManager.LoadScene("Level_1");

    }
    //public void CheckPoint()
    //{
    //    mainMenuUI.SetActive(false);
    //    creditsUI.SetActive(false);
    //    Cursor.visible = false;
    //    SceneManager.LoadScene(PlayerPrefs.GetString("SavedLevel"));
    //    SavePos = new Vector3(PlayerPrefs.GetFloat("SavePosx"), PlayerPrefs.GetFloat("SavePosy"), PlayerPrefs.GetFloat("SavePosz"));
    //    Saverot = new Quaternion(PlayerPrefs.GetFloat("SaveRotx"), PlayerPrefs.GetFloat("SaveRoty"), PlayerPrefs.GetFloat("SaveRotz"), PlayerPrefs.GetFloat("SaveRotw"));
    //    DontDestroyOnLoad(this.gameObject);
    //}
    public void Credits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
        Cursor.visible = true;
    }
    public void MusicCredits()
    {
        mainMenuUI.SetActive(false);
        MusicCreditsUI.SetActive(true);
        Cursor.visible = true;
    }
    public void Back()
    {
        mainMenuUI.SetActive(true);
        creditsUI.SetActive(false);
        MusicCreditsUI.SetActive(false);
        Cursor.visible = true;
    }
    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

    private void OnSceneLoaded()
    {
        Destroy(GameObject.Find(gameObject.name), 3);
    }
}