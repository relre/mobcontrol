using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Replicator : MonoBehaviour
{
    GameManager gameManager;
    public int replicatorNumber;
    public TextMeshProUGUI replicatorNumberText;
  
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        replicatorNumberText.text = "X" + replicatorNumber.ToString();
    }

    void Update()
    {
        
    }
}
