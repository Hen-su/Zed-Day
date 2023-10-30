using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    public GameUI gameUI;
    public PlayerController controller;
    public float zombieDamage;

    public int rifleCost;
    public int machineGunCost;

    private bool inBuyTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            transform.parent.transform.position = transform.parent.transform.position;
            Debug.Log("Collide with Zombie");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ZombieArms")
        {
            //Debug.Log("Hit by zombie");
            controller.DecreaseHealth(zombieDamage);
        }

        if (other.tag == "Rifle")
        {
            gameUI.BuyInteractionText("Rifle", rifleCost);
        }
        if (other.tag == "MachineGun")
        {
            gameUI.BuyInteractionText("MachineGun", machineGunCost);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rifle")
        {
            gameUI.RemoveInteractionText();
        }
        if (other.tag == "MachineGun")
        {
            gameUI.RemoveInteractionText();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Rifle")
        {
            inBuyTrigger = true;
            if (inBuyTrigger && Input.GetKeyDown(KeyCode.E))
            {
                BuyEvent("Rifle", rifleCost);
            }
        }

        if (other.tag == "MachineGun")
        {
            inBuyTrigger = true;
            if (inBuyTrigger && Input.GetKeyDown(KeyCode.E))
            {
                BuyEvent("MachineGun", machineGunCost);
            }
        }
    }

    void BuyEvent(string gun, int cost)
    {
        if (controller.money >= cost)
        {
            controller.ChangePrefab(gun);
            controller.money -= cost;
        }
        else
        {
            gameUI.InsufficientFundsText();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
