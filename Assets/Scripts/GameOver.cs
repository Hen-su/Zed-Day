using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button tryAgainBTN;
    public Button menuBTN;
    public AudioClip buttonAudio;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        tryAgainBTN.onClick.AddListener(LoadGame);
        menuBTN.onClick.AddListener(LoadStart);
    }

    void LoadGame()
    {
        AudioSource.PlayOneShot(buttonAudio);
        SceneManager.LoadScene("GameScene");
    }

    void LoadStart()
    {
        AudioSource.PlayOneShot(buttonAudio);
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
