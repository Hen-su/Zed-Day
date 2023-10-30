using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameManager gameManager;
    private GameObject player;
    public Vector3 cameraPos;
    private int childIndex;
    // Start is called before the first frame update
    void Start()
    {
        childIndex = 0;
    }

    public void GetActivePlayerPrefab()
    {
        GameObject player = GameObject.FindWithTag("Player");
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                childIndex = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.GetChild(childIndex).gameObject;
        transform.position = player.transform.GetChild(childIndex).gameObject.transform.position + cameraPos;
        transform.LookAt(player.transform.GetChild(childIndex).gameObject.transform);
        //Debug.Log(childIndex);
    }
}
