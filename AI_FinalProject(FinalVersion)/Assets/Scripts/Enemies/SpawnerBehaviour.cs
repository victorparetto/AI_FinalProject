using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject basicEnemy;
    GameManager manager;

    float spawnCounter = 0;
    public float enemyCounter = 0;
    public float timeBetweenSpawns;

    public int maxEnemiesOnScene = 1;

    public GameObject[] enemies;

    public int hitCounter;

    public Color[] colors;
    bool isInvincible;

    void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeBetweenSpawns = 2.5f;

    }
	
	void Update () {

            if (enemyCounter < maxEnemiesOnScene)
            {
                spawnCounter += Time.deltaTime;

                if (spawnCounter >= timeBetweenSpawns)
                {
                    Spawn();
                    spawnCounter = 0;
                    enemyCounter++;
                }
            }

	}

    void Spawn()
    {
        Instantiate(enemies[Random.Range(0,enemies.Length)], transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            if (!isInvincible)
            {
                StartCoroutine(Teleport());
                StartCoroutine(Invincible());
            }
        }
    }

    IEnumerator Teleport()
    {
        hitCounter++;
        manager.actualScore += 20;
        int chosenNod = Random.Range(0, PlayerMovement.path.Count);
        transform.position = new Vector3(PlayerMovement.path[chosenNod].x, PlayerMovement.path[chosenNod].y);

        yield return null;
    }

    IEnumerator Invincible()
    {
        isInvincible = true;

        GetComponent<SpriteRenderer>().material.color = colors[1];

        yield return new WaitForSeconds(2.5f);

        GetComponent<SpriteRenderer>().material.color = colors[0];

        isInvincible = false;

    }
}
