using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehav : MonoBehaviour {

    Vector3 direction;

	void Start () {

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        direction = (playerPos - transform.position).normalized;
	}
	
	void Update () {

        transform.Translate(direction * Time.deltaTime * 5);
	}
}
