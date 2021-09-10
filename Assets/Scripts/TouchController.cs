using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    GameManager gameManager;
    float touchSpeed = 0.01f;
    Vector3 firstMousePos, secondMousePos;
    float mouseSpeed = 10f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }
        if (Input.GetMouseButton(0))
        {
            secondMousePos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
            Vector3 diff = secondMousePos - firstMousePos;
            gameManager.cannon.transform.position += diff * mouseSpeed;
            firstMousePos = secondMousePos;
            CannonPositionChecker();
        }
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
                CannonPositionChecker();
            }
        }
    }

    // cannon position bound
    void CannonPositionChecker()
    {
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
