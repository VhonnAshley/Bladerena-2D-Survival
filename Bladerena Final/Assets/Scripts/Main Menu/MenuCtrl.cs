using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    // Declare game object
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
}
