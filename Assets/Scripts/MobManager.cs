using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobManager : MonoBehaviour
{
    GameManager gameManager;
    Replicator replicator;
    NavMeshAgent mobNavMeshAgent;

    Vector3 spawnPos;
    float otherPositionZDistance = 1f;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        replicator = GameObject.FindGameObjectWithTag("Replicator").GetComponent<Replicator>();
        mobNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        mobNavMeshAgent.SetDestination(gameManager.mobTarget.transform.position);
    }

    // tower trigger 
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Tower"))
        {
            gameManager.towerNumber--;
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("DangerZone"))
        {
            Destroy(gameObject);
        }
       
    }

    // replicator trigger
    private void OnTriggerExit(Collider other)
    {
        spawnPos = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + otherPositionZDistance);
        if (other.gameObject.CompareTag("Replicator"))
        {
            for (int i = 0; i < other.gameObject.GetComponent<Replicator>().replicatorNumber; i++)
            {
                if (gameObject.tag == "PlayerMob")
                {
                    Instantiate(gameManager.playerMob, spawnPos, Quaternion.identity);

                }
                else if (gameObject.tag == "PlayerSuperMob")
                {
                    Instantiate(gameManager.playerSuperMob, spawnPos, Quaternion.identity);
                }

            }
        }
    }
 
}
