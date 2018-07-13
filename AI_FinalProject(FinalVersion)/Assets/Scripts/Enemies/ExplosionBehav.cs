using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehav : MonoBehaviour {

    float counter;

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= 2.5f)
        {
            Destroy(gameObject);
        }
    }
}
