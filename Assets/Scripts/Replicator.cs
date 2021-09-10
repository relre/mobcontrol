using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Replicator : MonoBehaviour
{
    public int replicatorNumber;
    public TextMeshProUGUI replicatorNumberText;
  
    void Start()
    {
        replicatorNumberText.text = "X" + replicatorNumber.ToString();
    }

    void Update()
    {
        
    }
}
