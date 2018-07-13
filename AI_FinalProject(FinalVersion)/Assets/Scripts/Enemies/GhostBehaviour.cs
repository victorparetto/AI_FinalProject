using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour {

    GameObject player;
    GameManager manager;

    Vector3 velocity;
    Vector3 steer;
    Vector3 desired;

    float MAX_SPEED = 4f;
    float MAX_STEER = 0.1f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(manager.timerScore <= 100)
        {
            MAX_SPEED = 2;
            MAX_STEER = 0.1f;
        }
        else if(manager.timerScore > 100 && manager.timerScore <= 250)
        {
            MAX_SPEED = 3f;
            MAX_STEER = 0.3f;
        }
        else if(manager.timerScore > 250)
        {
            MAX_SPEED = 5f;
            MAX_STEER = 0.5f;
        }
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
        if(other.tag == "Sword")
        {
            manager.actualScore += 30;
            Destroy(gameObject);
        }
    }
}
