using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    GameManager gameManager;



    float touchSpeed = 0.01f;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > gameManager.cannonTimer)
        {
            gameManager.PlayerMobSpawner();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && Time.time > gameManager.cannonTimer)
            {
                gameManager.PlayerMobSpawner();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                gameManager.cannon.transform.position = new Vector3(
                    gameManager.cannon.transform.position.x + touch.deltaPosition.x * touchSpeed,
                    gameManager.cannon.transform.position.y,
                    gameManager.cannon.transform.position.z);

                // cannon position bound
                if (gameManager.cannon.transform.position.x > gameManager.cannonMaxPositionX)
                {
                    gameManager.cannon.transform.position = new Vector3(gameManager.cannonMaxPositionX, gameManager.cannon.transform.position.y, gameManager.cannon.transform.position.z);
                }
                if (gameManager.cannon.transform.position.x < -gameManager.cannonMaxPositionX)
                {
                    gameManager.cannon.transform.position = new Vector3(-gameManager.cannonMaxPositionX, gameManager.cannon.transform.position.y, gameManager.cannon.transform.position.z);
                }
            }
        }
    }

}
