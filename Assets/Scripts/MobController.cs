using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    public GameObject playerMob;
    public GameObject mobTarget;

    [SerializeField]
    private float playerMobSpeed;

    NavMeshAgent playerMobAgent;

    void Start()
    {
        playerMobAgent = playerMob.GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        playerMobAgent.SetDestination(mobTarget.transform.position);
    }
}
