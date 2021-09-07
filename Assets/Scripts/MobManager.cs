using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobManager : MonoBehaviour
{
    GameManager gameManager;
    NavMeshAgent mobNavMeshAgent;
    
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mobNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        mobNavMeshAgent.SetDestination(gameManager.mobTarget.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Tower"))
        {
            gameManager.towerNumber--;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Replicator"))
        {
            Instantiate(gameManager.playerMob, transform.position, Quaternion.identity);
            Instantiate(gameManager.playerMob, transform.position, Quaternion.identity);
        }
    }
    void MobSpawner()
    {
        
    }
}
