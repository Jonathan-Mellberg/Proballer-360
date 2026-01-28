using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public GameObject panel;

    basicMove playerMove;
    bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = gameObject.GetComponent<basicMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            PauseGame(panel);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            UnpauseGame(panel);
        }
    }

    private void PauseGame(GameObject pausePanel)
    {
        Time.timeScale = 0;
        paused = true;
        pausePanel.SetActive(true);
        playerMove.freeze = true;
        Cursor.visible = true;
    }

    private void UnpauseGame(GameObject pausePanel)
    {
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
        playerMove.freeze = false;
        Cursor.visible = false;
    }
}
