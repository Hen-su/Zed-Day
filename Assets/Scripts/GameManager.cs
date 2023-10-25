using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public int roundCount;
    public List<int> roundZombieCount;
    private int currentZombieCount;
    public float spawnDelay;

    public List<GameObject> ZomSpawns;
    public GameObject zombie;

    public List<GameObject> civilians;

    private int currentRound;
    private List<GameObject> currentZombieList = new List<GameObject>();
    private bool isSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        currentRound = 0;
        currentZombieCount = 0;
        isSpawn = false;
    }

    IEnumerator SpawnZombie()
    {
        while (currentZombieCount < roundZombieCount[currentRound - 1])
        {
            isSpawn = true;
            yield return new WaitForSeconds(spawnDelay);
            int randomSpawn = Random.Range(0, 4);
            GameObject instzombie = Instantiate(zombie, ZomSpawns[randomSpawn].transform.position, Quaternion.identity) as GameObject;
            currentZombieList.Add(instzombie);
            currentZombieCount++;
        }
        isSpawn = false;
    }

    void RoundComplete()
    {
        if (isSpawn == false && currentZombieList.Count == 0)
        {
            currentRound++;
            Debug.Log("Current Round"+ currentRound);
            StartCoroutine(SpawnZombie());
        }
    }

    // Update is called once per frame
    void Update()
    {
        RoundComplete();
    }
}
