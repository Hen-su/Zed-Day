using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public PlayerController playerController;
    
    public Slider health;
    public Slider stamina;
    public TextMeshProUGUI bullets;
    public TextMeshProUGUI money;
    public TextMeshProUGUI interaction;
    public TextMeshProUGUI round;

    private int activeChildIndex;
    // Start is called before the first frame update
    void Start()
    {
        money.text = "Money: $" + playerController.money.ToString();
        bullets.text = "Ammo: " + playerController.transform.GetChild(activeChildIndex).transform.Find("barrelEnd").GetComponent<Shooting>().currentAmmo.ToString();
        round.text = "Round: 0";
        interaction.text = "";
    }

    void UpdateHPAndSTM()
    {
        health.value = playerController.currentHealth / playerController.maxHealth;
        stamina.value = playerController.currentStamina / playerController.maxStamina;
    }

    public void RoundText(int roundCount)
    {
        round.text = $"Round: {roundCount}";
    }

    public void GetActivePlayerPrefab()
    {
        GameObject player = GameObject.FindWithTag("Player");
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                Debug.Log($"{i} is true");
                activeChildIndex = i;
            }
        }
    }

    public void BuyInteractionText(string gun, int cost)
    {
        interaction.text = $"Press 'E' to Buy {gun} for ${cost}";
    }

    public void InsufficientFundsText()
    {
        interaction.text = "You don't have enough money";
    }

    public void RemoveInteractionText()
    {
        interaction.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPAndSTM();
        money.text = "Money: $" + playerController.money.ToString();
        bullets.text = "Ammo: " + playerController.transform.GetChild(activeChildIndex).transform.Find("barrelEnd").GetComponent<Shooting>().currentAmmo.ToString();
        //Debug.Log(activeChildIndex);
    }
}
