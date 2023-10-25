using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Shooting shootscript;
    public PlayerController playerScript;
    public ZombieController zombieScript;

    public GameObject pausePanel;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Debug.Log("esc pressed");
            Time.timeScale = 0;
            shootscript.enabled = false;
            playerScript.enabled = false;
            zombieScript.enabled = false;
            
            pausePanel.SetActive(true);
            isPaused = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }
}
