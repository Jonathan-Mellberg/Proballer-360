using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UIElements;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem.Android;

public class menuManager : MonoBehaviour
{
    public GameObject settings_Panel;
    public GameObject level_Panel;

    public List<GameObject> menuButtons; // buttons on main menu
    //public List<GameObject> selectButtons; // buttons on panel
    public void MoveToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
    public void Show_settings(GameObject pannel)
    {
        settings_Panel.SetActive(true);

        foreach (GameObject button in menuButtons)
        {
            button.SetActive(false);
        }
        /*
        foreach (GameObject button in selectButtons)
        {
            button.SetActive(true);
        }
        */
    }

    public void HidePanel(GameObject pannel)
    {
         pannel.SetActive(false);

        foreach (GameObject button in menuButtons)
        {
            button.SetActive(true);
        }
        /*
        foreach (GameObject button in selectButtons)
        {
            button.SetActive(false);
        }
        */
    }
}