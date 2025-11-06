using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UIElements;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.InputSystem.Android;

public class menuManager : MonoBehaviour
{
    public GameObject panel;
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

    public void ShowPanel()
    {
        panel.SetActive(true);

        foreach(GameObject button in menuButtons)
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

    public void HidePanel()
    {
        panel.SetActive(false);

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