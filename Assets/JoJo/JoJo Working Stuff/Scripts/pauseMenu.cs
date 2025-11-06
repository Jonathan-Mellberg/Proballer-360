using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    basicMove playerMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = gameObject.GetComponent<basicMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PauseGame(GameObject pausePanel)
    {
        pausePanel.SetActive(true);
        playerMove.freeze = true;
    }
}
