using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMobManager : MonoBehaviour
{
    GameManager gameManager;
    NavMeshAgent enemyMobNavMeshAgent;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        enemyMobNavMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        enemyMobNavMeshAgent.SetDestination(gameManager.enemyTarget.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerMob"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
