using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyShoot : MonoBehaviour {

    bool canShoot;
    public GameObject enemyBullet;

    float counter; 

	void Start () {
		
	}

	void Update () {

        if (canShoot)
        {
            counter += Time.deltaTime;

            if(counter >= 1.2f)
            {
                Instantiate(enemyBullet, transform.position, Quaternion.identity);
                counter = 0;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canShoot = false;
        }
    }
}
