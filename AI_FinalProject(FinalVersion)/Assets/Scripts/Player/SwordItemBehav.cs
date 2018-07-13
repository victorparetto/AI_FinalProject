using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemBehav : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "EnemySpawner")
        {
            TeleportSword();
        }
    }

    public void TeleportSword()
    {
        int chosenNod = Random.Range(0, PlayerMovement.path.Count);
        transform.position = new Vector3(PlayerMovement.path[chosenNod].x, PlayerMovement.path[chosenNod].y);
    }
}
