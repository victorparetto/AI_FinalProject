using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPFBehaviour : MonoBehaviour {

    int[,] m = new int[,]
    {
//    0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19
    { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
    { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 1
    { 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 2 
    { 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 3
    { 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 4
    { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 5 
    { 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }, // 6 
    { 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // 7
    { 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 }, // 8
    { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 }, // 9
    { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0 }, // 10
    { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0 }, // 11
    { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0 }, // 12
    { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0 }, // 13
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 }, // 14
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0 }, // 15
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0 }, // 16
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0 }, // 17
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 }, // 18
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 }, // 19

    };

    private PlayerMovement playerM;

    public float t;

    public int initIndex = 0;
    public int endIndex = 0;

    public bool rangeEnemy;
    public bool meleeEnemy;

    void Start()
    {
        playerM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        float spawnerNearestNod = 9999;
        for (int i = 0; i < PlayerMovement.path.Count; i++)
        {
            if(Vector3.Distance(GameObject.Find("EnemySpawner").transform.position, PlayerMovement.path[i]) < spawnerNearestNod)
            {
                spawnerNearestNod = Vector3.Distance(GameObject.Find("EnemySpawner").transform.position, PlayerMovement.path[i]);
                initIndex = i;
            }
        }

        endIndex = initIndex;
    }

    void Update () {

        if (rangeEnemy)
        {
            if (endIndex != playerM.nearestNodIndex)
            {
                //Dentro del if se quedara en el nodo anterior al del player / fuera del if chocara al player
                transform.position = Vector3.Lerp(PlayerMovement.path[initIndex], PlayerMovement.path[endIndex], t);
                t += 0.02f;

                if (t >= 1)
                {
                    GetDijkstraValues();
                    t = 0;
                }
            }
        }

        if (meleeEnemy)
        {
                //Dentro del if se quedara en el nodo anterior al del player / fuera del if chocara al player
                transform.position = Vector3.Lerp(PlayerMovement.path[initIndex], PlayerMovement.path[endIndex], t);
                t += 0.02f;
            if (endIndex != playerM.nearestNodIndex)
            {

                if (t >= 1)
                {
                    GetDijkstraValues();
                    t = 0;
                }
            }
        }


    }

    void GetDijkstraValues()
    {
        int endDijkstra = playerM.nearestNodIndex;

        List<int> disjkstraPath = PathFinding.Disjkstra(m, endIndex, endDijkstra);

        initIndex = endIndex;
        endIndex = disjkstraPath[1];
      
    }
}
