using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    SceneChanger sceneChanger;

    public GameObject playerMob;
    public GameObject playerSuperMob;
    public GameObject mobTarget;

    GameObject[] enemyObjects;
    public GameObject enemyTarget;
    public GameObject enemyMob;
    public int enemyOneWaveLenght = 5;
    public float enemyOneWaveSpawnRate = 0.2f;
    public float enemyNextWaveSpawnTime = 2f;
    public int enemyTotalMobLenght = 30;

    public GameObject tower;
    public TextMeshProUGUI towerNumberText;
    public int towerNumber = 100;

    public GameObject cannon;
    public float cannonFireRate = 0.2f;
    public float cannonMaxPositionX = 2.33f;
    public float cannonTimer;
    Vector3 cannonSpawnPosition;
    float cannonSpawnDistance = 0.8f;
    float cannonEnergy = 0f;
    float cannonEnergyMax = 10f;
    public Image cannonEnergyImage;

    public Material gold;
    public Material black;
  
    public int score = 0;
    public int scoreEnemyDead = 1;
    public int scoreTowerPlayerMob = 2;
    public int scoreTowerSuperMob = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;

    public bool isGameActive;
    public GameObject endLevelPanelUI;
    public GameObject failedLevelPanelUI;
    public GameObject homePanelUI;

    void Start()
    {
        isGameActive = true;
        sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneChanger>();
        LevelDiffuculty();
        LevelNumberUI();

    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        towerNumberText.text = towerNumber.ToString();
        CannonEnergy();
        EnemyWaveSpawner();
        CannonPositionCalculator();
        LevelController();
    }

    // id to level id
    string levelID(int id) 
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }
   // level diffuculty settings
    void LevelDiffuculty()
    {
        string currentLevel = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]);
        if (currentLevelID == 1)
        {
            towerNumber = 42;
            enemyOneWaveLenght = 3;
        }
        else if (currentLevelID == 2)
        {
            towerNumber = 55;
            enemyOneWaveLenght = 4;
        }
        else if (currentLevelID == 3)
        {
            towerNumber = 62;
            enemyOneWaveLenght = 4;
        }
        else if (currentLevelID == 4)
        {
            towerNumber = 87;
            enemyOneWaveLenght = 4;
        }
        else if (currentLevelID == 5)
        {
            towerNumber = 99;
            enemyOneWaveLenght = 5;
        }

    }
    void LevelController()
    {
        // win this level
        if (towerNumber <= 0)
        {
            isGameActive = false;
            NextLevelController();
        }

        // game pause or finish
        if (isGameActive == false)
        {
            GameObject[] clearPlayerMobs = GameObject.FindGameObjectsWithTag("PlayerMob");
            for (int i = 0; i < clearPlayerMobs.Length; i++)
            {
                Destroy(clearPlayerMobs[i]);
            }
            GameObject[] clearSuperMobs = GameObject.FindGameObjectsWithTag("PlayerSuperMob");
            for (int i = 0; i < clearSuperMobs.Length; i++)
            {
                Destroy(clearSuperMobs[i]);
            }
            GameObject[] clearEnemyMobs = GameObject.FindGameObjectsWithTag("EnemyMob");
            for (int i = 0; i < clearEnemyMobs.Length; i++)
            {
                Destroy(clearEnemyMobs[i]);
            }
        }


    }
    void NextLevelController()
    {
        string currentLevel = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]);
        int nextLevel = PlayerPrefs.GetInt("level") + 1;
            if (nextLevel - currentLevelID == 1)
            {
                PlayerPrefs.SetInt("level", nextLevel);
            }
        EndLevelPanel(); // end level panel open
    }
    public void FailedLevelPanel()
    {
        isGameActive = false;
        failedLevelPanelUI.SetActive(true);
    }
    public void EndLevelPanel()
    {
        isGameActive = false;
        endLevelPanelUI.SetActive(true);
    }
    public void HomePanel()
    {
        isGameActive = false;
        homePanelUI.SetActive(true);
    }

    // level number
    void LevelNumberUI() 
    {
        string currentLevel = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]);
        levelText.text = "Level: " + currentLevelID;
    }

    // next level button
    public void NextLevel() 
    {
        sceneChanger.SceneChange(levelID(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // play again button
    public void PlayAgain() 
    {
        sceneChanger.SceneChange(levelID(SceneManager.GetActiveScene().buildIndex));
    }

    // select login scene
    public void HomeButton()
    {
        SceneManager.LoadScene("login");
    }
    void CannonPositionCalculator()
    {
        cannonSpawnPosition = new Vector3(cannon.transform.position.x, cannon.transform.position.y, cannon.transform.position.z + cannonSpawnDistance);
    }
    void CannonEnergy()
    {
        cannonEnergyImage.fillAmount = cannonEnergy / cannonEnergyMax;
        if (cannonEnergyImage.fillAmount == 0.9f)
        {
            cannon.GetComponent<Renderer>().material = gold;
        }
        else
        {
            cannon.GetComponent<Renderer>().material = black;
        }
    }
    public void PlayerMobSpawner()
    {
        cannonTimer = Time.time + cannonFireRate;
        cannonEnergy++;
        if (cannonEnergy < cannonEnergyMax && isGameActive == true)
        {
            Instantiate(playerMob, cannonSpawnPosition, Quaternion.identity);
        }
        else if (cannonEnergy == cannonEnergyMax && isGameActive == true)
        {
            Instantiate(playerSuperMob, cannonSpawnPosition, Quaternion.identity);
            cannonEnergy = 0;
        }
    }

    public void EnemyMobSpawner()
    {
        Vector3 enemyMobPosition = new Vector3(tower.transform.position.x, 0.5f, tower.transform.position.z - 2);
        Quaternion enemyMobRotation = new Quaternion(transform.position.x, 180, transform.position.z, transform.position.z);

        for (int i = 0; i < enemyOneWaveLenght; i++)
        {
            Instantiate(enemyMob, enemyMobPosition, enemyMobRotation);
        }
    }
    public void EnemyWaveSpawner()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("EnemyMob");

        if (enemyObjects.Length == 0 && isGameActive == true)
        {
            InvokeRepeating("EnemyMobSpawner", enemyNextWaveSpawnTime, enemyOneWaveSpawnRate);
        }
        if (enemyObjects.Length >= enemyTotalMobLenght)
        {
            CancelInvoke();
        }
    }
}
