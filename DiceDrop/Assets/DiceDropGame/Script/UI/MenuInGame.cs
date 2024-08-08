using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuInGame : MonoBehaviour
{
    public GameObject MenuUi;
    public GameObject ButtonUi;

    public AudioSource Music;


    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void OpenMiniMenu(GameObject open)
    {
        Time.timeScale = 0f;
        open.SetActive(true);
        MenuUi.SetActive(false);
        ButtonUi.GetComponent<Button>().interactable = false;
    }
    public void ReternInGame(GameObject closed)
    {
        Time.timeScale = 1f;
        closed.SetActive(false);
        MenuUi.SetActive(true);
        ButtonUi.GetComponent<Button>().interactable = true;
    }


    public void SceneInMenu()
    {
        SceneManager.LoadScene(0);
        YandexGame.FullscreenShow();
    }

    public void SetMusicEnamled(bool value)
    {
        Music.enabled = value;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
   
}
