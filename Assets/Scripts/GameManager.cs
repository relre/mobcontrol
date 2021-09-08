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

    Vector3 cannonSpawnPosition;
    float cannonSpawnDistance = 0.8f;

    public Image cannonEnergyImage;
    public Material gold;
    public Material black;
    float cannonEnergy = 0f;
    float cannonEnergyMax = 10f;
    


    void Start()
    {
        cannonSpawnPosition = new Vector3(cannon.transform.position.x, cannon.transform.position.y, cannon.transform.position.z + cannonSpawnDistance);
    }

    void Update()
    {
        towerNumberText.text = towerNumber.ToString();
        CannonEnergy();
    }

    void CannonEnergy()
    {
        cannonEnergyImage.fillAmount = cannonEnergy / cannonEnergyMax;
        if (cannonEnergyImage.fillAmount == 0.9f)
        {
            cannon.GetComponent<Renderer>().material = gold;
        }
        else
        {
            cannon.GetComponent<Renderer>().material = black;
        }
    }
    public void PlayerMobSpawner()
    {
        cannonNextFire = Time.time + cannonFireRate;
        cannonEnergy++;
        if (cannonEnergy < cannonEnergyMax)
        {
            Instantiate(playerMob, cannonSpawnPosition, Quaternion.identity);
        }
        else if (cannonEnergy == cannonEnergyMax)
        {
            Instantiate(playerSuperMob, cannonSpawnPosition, Quaternion.identity);
            cannonEnergy = 0;
        }
        
    }
}
