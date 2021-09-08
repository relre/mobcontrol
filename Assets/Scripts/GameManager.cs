using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerMob;
    public GameObject playerSuperMob;
    public GameObject mobTarget;
    public GameObject cannon;

    public TextMeshProUGUI towerNumberText;
    public int towerNumber = 100;
    public float cannonFireRate = 0.2f;
    public float cannonMaxPositionX = 2.33f;
    public float cannonNextFire;

    public Image powerImage;
    float powerForce = 0f;
    float powerForceMax = 10f;


    void Start()
    {
        
    }

    void Update()
    {
        towerNumberText.text = towerNumber.ToString();
        PowerEnergy();
    }

    void PowerEnergy()
    {
        powerImage.fillAmount = powerForce / powerForceMax;
    }
    public void PlayerMobSpawner()
    {
        cannonNextFire = Time.time + cannonFireRate;
        powerForce++;
        if (powerForce < powerForceMax)
        {
            Instantiate(playerMob, cannon.transform.position, Quaternion.identity);
        }
        else if (powerForce == powerForceMax)
        {
            Instantiate(playerSuperMob, cannon.transform.position, Quaternion.identity);
            powerForce = 0;
        }
        
    }
}
