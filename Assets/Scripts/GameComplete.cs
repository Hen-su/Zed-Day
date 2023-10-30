using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameComplete : MonoBehaviour
{
    public TextMeshProUGUI survivors;
    public TextMeshProUGUI money;
    public Button tryAgainBTN;
    public Button menuBTN;
    public AudioClip buttonAudio;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        tryAgainBTN.onClick.AddListener(LoadGame);
        menuBTN.onClick.AddListener(LoadStart);
        survivors.text = "Survivors Rescued: " + PlayerPrefs.GetInt("Survivors");
        money.text = "Money Earned: " + PlayerPrefs.GetInt("Money");
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
