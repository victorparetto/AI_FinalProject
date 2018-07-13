using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour {

    GameManager manager;

    int timeToSpawnGhost;
    int numberOfHitsNeeded;
    float timerToSpawnGhost;

    public GameObject ghost;

    public Color[] colors;

	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(manager.timerScore <= 100)
        {
            timeToSpawnGhost = 10;
            numberOfHitsNeeded = 5;
        }

        else if(manager.timerScore > 100)
        {
            timeToSpawnGhost = 5;
            numberOfHitsNeeded = 10;
        }
	}
	
	void Update () {
        timerToSpawnGhost += Time.deltaTime;

        if(timerToSpawnGhost >= timeToSpawnGhost)
        {
            SpawnGhost();
        }

        if (numberOfHitsNeeded == 0)
        {
            manager.actualScore += 60;
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Sword")
        {
            StartCoroutine(GotHit());
        }
    }

    void SpawnGhost()
    {
        Instantiate(ghost, transform.position, transform.rotation);
        timerToSpawnGhost = 0;
    }

    IEnumerator GotHit()
    {
        numberOfHitsNeeded--;
        GetComponent<SpriteRenderer>().material.color = colors[1];

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().material.color = colors[0];
    }
}
