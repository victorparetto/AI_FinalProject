using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitboxScript : MonoBehaviour {

    GameManager score;
    SpawnerBehaviour spawner;

	void Start () {

        score = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>();
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            spawner.enemyCounter -= 1;
            score.actualScore += 10;
        }

        if(other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
