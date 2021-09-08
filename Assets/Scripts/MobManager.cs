using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobManager : MonoBehaviour
{
    GameManager gameManager;
    NavMeshAgent mobNavMeshAgent;

    Vector3 spawnPos;
    float otherPositionZDistance = 1f;
    
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
       
    }
    private void OnTriggerExit(Collider other)
    {
        spawnPos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + otherPositionZDistance);
        if (other.gameObject.CompareTag("Replicator"))
        {

            for (int i = 0; i < 4; i++)
            {
                Instantiate(gameManager.playerMob, spawnPos, Quaternion.identity);
            }
            


        }
    }
 
}
