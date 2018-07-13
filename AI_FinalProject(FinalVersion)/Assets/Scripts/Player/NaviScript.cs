using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviScript : MonoBehaviour {

    GameObject player;
    GameManager manager;
    SpawnerBehaviour spawner;

    Vector3 velocity;
    Vector3 steer;
    Vector3 desired;

    float MAX_SPEED = 20f;
    float MAX_STEER = 0.1f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>();
    }

    void Update()
    {

        desired = (player.transform.position - transform.position).normalized * MAX_SPEED;

        steer = Vector3.ClampMagnitude(desired - velocity, MAX_STEER);

        velocity += steer;

        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            manager.actualScore += 10;
            spawner.enemyCounter -= 1;
        }

        if(other.tag == "Ghost")
        {
            Destroy(other.gameObject);
            manager.actualScore += 30;
        }

        if(other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
