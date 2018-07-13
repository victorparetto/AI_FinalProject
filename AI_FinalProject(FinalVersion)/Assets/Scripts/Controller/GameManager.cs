using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    SpawnerBehaviour spawner;
    PlayerMovement player;

    public Text scoreT;
    public int actualScore;

    public Text timerT;
    public float timerScore;

    public GameObject swordItem;
    public Text swordTimer;
    public float timerSwordLeft;

    int spawnGhostSpawnerTimer;
    float ghostSpawnerCounter;
    public GameObject ghostSpawnerObject;

    public GameObject[] items;
    float itemSpawnCounter;
    int timerToSpawnItem;
    int itemsDropRate;

    public GameObject navi;
    public GameObject darkNavi;
    public bool hasNavi = false;
    public bool hasDarkNavi = false;

    public GameObject pauseMenu;

    void Start () {
        spawner = GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        timerSwordLeft = 0;
        spawnGhostSpawnerTimer = 20;
        timerToSpawnItem = Random.Range(20, 30);
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < allEnemies.Length; i++)
            {
                allEnemies[i].GetComponent<EnemyPFBehaviour>().enabled = false;
            }
            if(actualScore >= 500)
            {
                GameObject.Find("Navi").GetComponent<NaviScript>().enabled = false;
            }
            player.enabled = false;
            GetComponent<GameManager>().enabled = false;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

       timerScore += Time.deltaTime;

        scoreT.text = "Score: " + actualScore;
        timerT.text = "" + (int)timerScore;

        if (player.grabbedSword)
        {
            swordTimer.text = "" + (int)timerSwordLeft;
            timerSwordLeft -= Time.deltaTime;

        }
        if(timerSwordLeft <= 0)
        {
            timerSwordLeft = 0;
            player.grabbedSword = false;
            swordItem.SetActive(true);
            swordTimer.text = "0";
        }

        ghostSpawnerCounter += Time.deltaTime;
        if(ghostSpawnerCounter >= spawnGhostSpawnerTimer)
        {
            SpawnGhostSpawner();
        }

        itemSpawnCounter += Time.deltaTime;
        if(itemSpawnCounter >= timerToSpawnItem)
        {
            SpawnItem();
        }

        if(actualScore >= 500 && !hasNavi)
        {
            navi.SetActive(true);
            hasNavi = true;
        }

        if (actualScore >= 1000 && !hasDarkNavi)
        {
            darkNavi.SetActive(true);
            darkNavi.transform.parent = null;
            hasDarkNavi = true;
        }

        if(timerScore >= 50)
        {
            spawner.timeBetweenSpawns = 2;
            spawner.maxEnemiesOnScene = 3;
        }

        if (timerScore >= 100)
        {
            spawner.timeBetweenSpawns = 1.5f;
            spawner.maxEnemiesOnScene = 5;
        }

        if (timerScore >= 150)
        {
            spawner.timeBetweenSpawns = 1f;
            spawner.maxEnemiesOnScene = 7;
        }
    }

    void SpawnGhostSpawner()
    {
        GameObject[] spawnersInScene = GameObject.FindGameObjectsWithTag("GhostSpawner");

        if (spawnersInScene.Length == 0)
        {
            if (timerScore <= 100)
            {
                spawnGhostSpawnerTimer = Random.Range(20, 50);
            }
            else if(timerScore > 100)
            {
                spawnGhostSpawnerTimer = Random.Range(10, 40);
            }

            Instantiate(ghostSpawnerObject, new Vector3(7.5f, Random.Range(-3.5f, 3.5f), 0), transform.rotation);
            ghostSpawnerCounter = 0;
        }
    }

    void SpawnItem()
    {
        itemsDropRate = Random.Range(1, 100);

        if (itemsDropRate <= 45)
        {
            Instantiate(items[0], new Vector3(4.4f, -6, 0), transform.rotation);
        }
        else if(itemsDropRate > 45 && itemsDropRate <= 80)
        {
            Instantiate(items[1], new Vector3(4.4f, -6, 0), transform.rotation);
        }
        else if (itemsDropRate > 80)
        {
            Instantiate(items[2], new Vector3(4.4f, -6, 0), transform.rotation);
        }

        timerToSpawnItem = Random.Range(20, 45);
        itemSpawnCounter = 0;
    }

    public void UnPauseGame()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < allEnemies.Length; i++)
        {
            allEnemies[i].GetComponent<EnemyPFBehaviour>().enabled = true;
        }
        if (actualScore >= 500)
        {
            GameObject.Find("Navi").GetComponent<NaviScript>().enabled = true;
        }
        Time.timeScale = 1;
        player.enabled = true;
        GetComponent<GameManager>().enabled = true;
        pauseMenu.SetActive(false);
    }
}
