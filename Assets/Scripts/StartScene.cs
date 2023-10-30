using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Button startBTN;
    public Button tutorialBTN;
    public Button quitBTN;
    public Button closeBTN;
    public AudioClip buttonAudio;
    public AudioSource AudioSource;

    public GameObject tutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        startBTN.onClick.AddListener(LoadGame);
        tutorialBTN.onClick.AddListener(TutorialPanel);
        quitBTN.onClick.AddListener(Quit);
        closeBTN.onClick.AddListener(ClosePanel);
    }

    void LoadGame()
    {
        AudioSource.PlayOneShot(buttonAudio);
        SceneManager.LoadScene("GameScene");
    }

    void TutorialPanel()
    {
        AudioSource.PlayOneShot(buttonAudio);
        tutorialPanel.SetActive(true);
    }

    void ClosePanel()
    {
        AudioSource.PlayOneShot(buttonAudio);
        tutorialPanel.SetActive(false);
    }

    void Quit()
    {
        AudioSource.PlayOneShot(buttonAudio);
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
