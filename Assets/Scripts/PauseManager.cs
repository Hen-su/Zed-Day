using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameManager gameManager;
    public PlayerController playerScript;
    //public ZombieController zombieScript;
    public CivilianFollow CivilianScript;
    public PoliceController policeScript;

    public GameObject pausePanel;

    private Shooting shootingScript;


    public Button resume;
    public Button restart;
    public Button menu;
    public Slider volume;

    public AudioClip buttonAudio;

    private bool isPaused = false;
    private int activeChildIndex;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        resume.onClick.AddListener(Unpause);
        restart.onClick.AddListener(Restart);
        menu.onClick.AddListener(LoadStart);
        audioSource.volume = volume.value;
        volume.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    void GetActivePlayerPrefab()
    {
        GameObject player = GameObject.FindWithTag("Player");
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                activeChildIndex = i;
                GameObject prefab = player.transform.GetChild(i).gameObject;
                shootingScript = prefab.transform.GetChild(prefab.transform.childCount-1).GetComponent<Shooting>();
            }
        }
    }

    void LoadStart()
    {
        audioSource.PlayOneShot(buttonAudio);
        SceneManager.LoadScene("StartScene");
    }

    void Restart()
    {
        audioSource.PlayOneShot(buttonAudio);
        SceneManager.LoadScene("GameScene");
    }

    void ChangeVolume()
    {
        audioSource.volume = volume.value;
    }

    void Unpause()
    {
        audioSource.PlayOneShot(buttonAudio);
        if (isPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            playerScript.enabled = true;
            //zombieScript.enabled = true;
            CivilianScript.enabled = true;
            policeScript.enabled = true;
            shootingScript.enabled = true;
            isPaused = false;
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            //Debug.Log("Pause");
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            playerScript.enabled = false;
            //zombieScript.enabled = false;
            CivilianScript.enabled = false;
            policeScript.enabled = false;

            GetActivePlayerPrefab();
            shootingScript.enabled = false;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            playerScript.enabled = true;
            //zombieScript.enabled = true;
            CivilianScript.enabled = true;
            policeScript.enabled = true;
            shootingScript.enabled = true;
            isPaused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }
}
