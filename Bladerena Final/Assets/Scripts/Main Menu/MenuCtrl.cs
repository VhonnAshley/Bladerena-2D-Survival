using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    // Declaration for music control in _Menu
    [SerializeField] AudioSource music;

    // Declare game object for showing-hiding buttons
    public GameObject objectToShow;
    public GameObject objectToHide;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnShowObjectButtonClick()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }

    public void OnHideObjectButtonClick()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }

    public void onMusic()
    {
        music.Play();
    }
    public void offMusic()
    {
        music.Stop();
    }

}
