using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    GameManager manager;
    PlayerMovement player;
    SpawnerBehaviour spawner;

    public bool isLife;
    public bool isTriforce;
    public bool isSwordBuff;

    List<Vector3> startPos = new List<Vector3>();
    int chosenPos;
    float t;

    public GameObject explosion;

	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>();

        startPos.Add(new Vector3(4.4f, -6, 0));
        startPos.Add(new Vector3(4.4f, 6, 0));

        chosenPos = Random.Range(0, startPos.Count);
        transform.position = startPos[chosenPos];
    }
	
	void Update () {
        t += Time.deltaTime * 0.1f;

        if (chosenPos == 0)
        {
            transform.position = Vector3.Lerp(startPos[0], startPos[1], t);
        }

        else if(chosenPos == 1)
        {
            transform.position = Vector3.Lerp(startPos[1], startPos[0], t);
        }

        if(t >= 1)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword" || other.tag == "Navi")
        {
            if (isLife)
            {
                player.lifes += 1;
                Destroy(gameObject);
            }
            else if (isTriforce)
            {
                DestroyAllEnemies();
            }
            else if (isSwordBuff)
            {
                if (player.grabbedSword)
                {
                    manager.timerSwordLeft = 45;
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] allBasicEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allGhostEnemies = GameObject.FindGameObjectsWithTag("Ghost");

        for (int i = 0; i < allBasicEnemies.Length; i++)
        {
            spawner.enemyCounter -= 1;
            Destroy(allBasicEnemies[i]);
            Instantiate(explosion, allBasicEnemies[i].transform.position, transform.rotation);
        }

        for (int i = 0; i < allGhostEnemies.Length; i++)
        {
            Destroy(allGhostEnemies[i]);
            Instantiate(explosion, allGhostEnemies[i].transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
