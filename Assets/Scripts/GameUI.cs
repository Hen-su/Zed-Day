using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameUI : MonoBehaviour
{
    public PlayerController playerController;
    public Shooting Shooting;
    public TextMeshProUGUI health;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI bullets;

    // Start is called before the first frame update
    void Start()
    {
        health.text = "Health: " + playerController.currentHealth.ToString();
        stamina.text = "Stamina: " + playerController.currentStamina.ToString();
        bullets.text = "Ammo: " + Shooting.currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + playerController.currentHealth.ToString();
        stamina.text = "Stamina: " + playerController.currentStamina.ToString();
        bullets.text = "Ammo: " + Shooting.currentAmmo.ToString();
    }
}
