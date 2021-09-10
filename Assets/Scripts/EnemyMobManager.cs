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
        // set enemy agent target
        enemyMobNavMeshAgent.SetDestination(gameManager.enemyTarget.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerMob"))
        {
            gameManager.score += gameManager.scoreEnemyDead;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("PlayerSuperMob"))
        {
            Vector3 superMobDefaultScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (other.gameObject.transform.localScale == superMobDefaultScale)
            {
                gameManager.score += gameManager.scoreEnemyDead;
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                Vector3 superMobDamageScale = new Vector3(0.1f, 0.1f, 0.1f);
                other.gameObject.transform.localScale = other.gameObject.transform.localScale - superMobDamageScale;
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("EnemyTarget"))
        {
            gameManager.isGameActive = false;
            gameManager.failedLevelPanelUI.SetActive(true);
        }
    }
}
