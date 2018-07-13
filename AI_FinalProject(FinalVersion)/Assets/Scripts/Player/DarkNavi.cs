using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNavi : MonoBehaviour
{

    GameObject player;
    GameManager manager;
    SpawnerBehaviour spawner;

    Vector3 velocity;
    Vector3 steer;
    Vector3 desired;

    float MAX_SPEED = 5f;
    float MAX_STEER = 0.7f;

    bool isAttacking;
    bool isWithPlayer;
    bool isCalculatingTarget;
    GameObject target;

    float counter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawner = GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>();
        isWithPlayer = true;
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 4)
        {
            CalcuteTarget();
            counter = 0;
        }

        if (isWithPlayer)
        {
            desired = (new Vector3(player.transform.position.x - 0.8f,player.transform.position.y + 0.8f) - transform.position).normalized * MAX_SPEED;

            steer = Vector3.ClampMagnitude(desired - velocity, MAX_STEER);

            velocity += steer;

            transform.position += velocity * Time.deltaTime;
        }

        else if (isAttacking)
        {
            desired = (target.transform.position - transform.position).normalized * MAX_SPEED;

            steer = Vector3.ClampMagnitude(desired - velocity, MAX_STEER);

            velocity += steer;

            transform.position += velocity * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            manager.actualScore += 10;
            spawner.enemyCounter -= 1;
            isAttacking = false;
            isWithPlayer = true;
        }

        if (other.tag == "Ghost")
        {
            Destroy(other.gameObject);
            manager.actualScore += 30;
            isAttacking = false;
            isWithPlayer = true;
        }

        if (other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }

    void CalcuteTarget()
    {
        if (GameObject.FindGameObjectWithTag("Ghost"))
        {
            target = GameObject.FindGameObjectWithTag("Ghost");
            isAttacking = true;
            isWithPlayer = false;
        }
        else if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
            isAttacking = true;
            isWithPlayer = false;
        }
        else if(!GameObject.FindGameObjectWithTag("Ghost") && !GameObject.FindGameObjectWithTag("Enemy"))
        {
            isAttacking = false;
            isWithPlayer = true;
        }
    }
}