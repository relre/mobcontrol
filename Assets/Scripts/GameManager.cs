using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerMob;
    public GameObject mobTarget;
    public GameObject cannon;

    public TextMeshProUGUI towerNumberText;
    public int towerNumber = 100;
    public float cannonFireRate = 0.2f;

    void Start()
    {

    }

    void Update()
    {
        towerNumberText.text = towerNumber.ToString();
    }

}
