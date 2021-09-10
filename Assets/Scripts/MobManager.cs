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
        MobTargetDestination();
    }

    // set player agent target
    void MobTargetDestination()
    {
        mobNavMeshAgent.SetDestination(gameManager.mobTarget.transform.position);
    }

    // tower trigger 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            gameManager.towerNumber--;
            if (gameObject.tag == "PlayerMob")
            {
                gameManager.score += gameManager.scoreTowerPlayerMob;
            }
            else if (gameObject.tag == "PlayerSuperMob")
            {
                gameManager.score += gameManager.scoreTowerSuperMob;
            }
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
