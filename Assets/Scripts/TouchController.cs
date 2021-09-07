using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    GameManager gameManager;
    
    float cannonNextFire;
    float cannonMaxPositionX = 4.5f;

    float touchSpeed = 0.01f;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cannonNextFire)
        {
            cannonNextFire = Time.time + gameManager.cannonFireRate;
            PlayerMobSpawner();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && Time.time > cannonNextFire)
            {
                cannonNextFire = Time.time + gameManager.cannonFireRate;

                Instantiate(gameManager.playerMob, gameManager.cannon.transform.position, Quaternion.identity);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                gameManager.cannon.transform.position = new Vector3(
                    gameManager.cannon.transform.position.x + touch.deltaPosition.x * touchSpeed,
                    gameManager.cannon.transform.position.y,
                    gameManager.cannon.transform.position.z);

                // cannon position bound
                if (gameManager.cannon.transform.position.x > cannonMaxPositionX)
                {
                    gameManager.cannon.transform.position = new Vector3(cannonMaxPositionX, gameManager.cannon.transform.position.y, gameManager.cannon.transform.position.z);
                }
                if (gameManager.cannon.transform.position.x < -cannonMaxPositionX)
                {
                    gameManager.cannon.transform.position = new Vector3(-cannonMaxPositionX, gameManager.cannon.transform.position.y, gameManager.cannon.transform.position.z);
                }
            }
        }
    }
    void PlayerMobSpawner()
    {
        Instantiate(gameManager.playerMob, gameManager.cannon.transform.position, Quaternion.identity);
    }
}
