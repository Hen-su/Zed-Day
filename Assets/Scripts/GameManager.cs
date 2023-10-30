using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public PlayerController controller;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public int roundCount;
    public List<int> roundZombieCount;
    public float spawnDelay;

    public List<GameObject> ZomSpawns;
    public GameObject zombie;
    public GameObject megaZombie;

    public List<CharacterController> followingList;
    public Scene gameComplete;
    public AudioSource audio;
    public float volume;

    private int currentRound;
    public List<GameObject> currentZombieList = new List<GameObject>();
    private bool isSpawn;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        currentRound = 0;
        isSpawn = false;
    }

    IEnumerator SpawnZombie()
    {
        while (currentZombieList.Count < roundZombieCount[currentRound - 1])
        {
            isSpawn = true;
            yield return new WaitForSeconds(spawnDelay);
            int randomSpawn = Random.Range(0, 4);
            GameObject instzombie = Instantiate(zombie, ZomSpawns[randomSpawn].transform.position, Quaternion.identity) as GameObject;
            currentZombieList.Add(instzombie);
        }
        if (currentRound == 5)
        {
            yield return new WaitForSeconds(spawnDelay);
            int randomSpawn = Random.Range(0, 4);
            GameObject instzombie = Instantiate(megaZombie, ZomSpawns[randomSpawn].transform.position, Quaternion.identity) as GameObject;
            currentZombieList.Add(instzombie);
        }
        isSpawn = false;
    }

    void RemoveDeadZombies()
    {
        foreach (GameObject inst in currentZombieList)
        {
            if (inst == null)
            {
                currentZombieList.Remove(inst);
            }
        }
    }

    int GetFollowingNPCs()
    {
        int survirorCount = 0;
        foreach (CharacterController c in followingList)
        {
            if (c != null)
            {
                survirorCount++;
            }
        }
        return survirorCount;
    }

    void RoundComplete()
    {
        if (isSpawn == false && currentZombieList.Count == 0 && currentRound < roundCount)
        {
            currentRound++;
            gameUI.RoundText(currentRound);
            StartCoroutine(SpawnZombie());
        }
        if (isSpawn == false && currentZombieList.Count == 0 && currentRound == roundCount)
        {
            PlayerPrefs.SetInt("Money", controller.money);
            PlayerPrefs.SetInt("Survivors", GetFollowingNPCs());
            SceneManager.LoadScene("GameCompleteScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        RemoveDeadZombies();
        RoundComplete();
        //Debug.Log("ZombieList" + currentZombieList.Count.ToString());
        //Debug.Log(childIndex);
        //Debug.Log($"Round {currentRound}");
    }
}
