using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + cameraPos;
        transform.LookAt(player);
    }
}
