using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBehaviour : MonoBehaviour {

    float t = 0;
    int initIndex = 0;
    int endIndex = 0;

    public List<Vector3> path = new List<Vector3>();
    Waypoint waypoint;

    public int[][] points = new int[][] {

       new int [] {1, 5}, //0
       new int [] {2, 6}, //1
       new int [] {1, 7}, //2
       new int [] {2, 8}, //3
       new int [] {3, 9}, //4
       new int [] {6, 10}, //5
       new int [] {1, 5, 7, 11}, //6
       new int [] {2, 6, 8, 12}, //7
       new int [] {3, 7, 9, 13}, //8
       new int [] {4, 8, 14}, //9
       new int [] {5, 11, 15}, //10
       new int [] {6, 10, 12, 16}, //11
       new int [] {7, 11, 13, 17}, //12
       new int [] {8, 12, 14, 18}, //13
       new int [] {9, 13, 19}, //14
       new int [] {10, 16}, //15
       new int [] {11, 15, 17}, //16
       new int [] {12, 16, 18}, //17
       new int [] {13, 17, 19}, //18
       new int [] {14, 18} //19
    };

    void Start () {

        GameObject.Find("GameManager").GetComponent<Waypoint>();

        path.Add(new Vector3(-7.68f, 3.85f)); //0
        path.Add(new Vector3(-7.68f, 1.85f)); //1
        path.Add(new Vector3(-7.68f, -0.15f)); //2
        path.Add(new Vector3(-7.68f, -2.15f)); //3
        path.Add(new Vector3(-7.68f, -4.15f)); //4
        path.Add(new Vector3(-5.5f, 3.85f)); //5
        path.Add(new Vector3(-5.5f, 1.85f)); //6
        path.Add(new Vector3(-5.5f, -0.15f)); //7
        path.Add(new Vector3(-5.5f, -2.15f)); //8
        path.Add(new Vector3(-5.5f, -4.15f)); //9
        path.Add(new Vector3(-3.4f, 3.85f)); //10
        path.Add(new Vector3(-3.4f, 1.85f)); //11
        path.Add(new Vector3(-3.4f, -0.15f)); //12
        path.Add(new Vector3(-3.4f, -2.15f)); //13
        path.Add(new Vector3(-3.4f, -4.15f)); //14
        path.Add(new Vector3(-1.25f, 3.85f)); //15
        path.Add(new Vector3(-1.25f, 1.85f)); //16
        path.Add(new Vector3(-1.25f, -0.15f)); //17
        path.Add(new Vector3(-1.25f, -2.15f)); //18
        path.Add(new Vector3(-1.25f, -4.15f)); //19

        Next();
	}
	
	void Update () {

        transform.position = Vector3.Lerp(path[initIndex], path[endIndex], t);
        t += 0.02f;

        if (t >= 1)
        {
            t = 0;
            Next();
        }
    }

    void Next()
    {
        int vecino = points[endIndex][Random.Range(0, points[endIndex].Length)];

        initIndex = endIndex;
        endIndex = vecino;

    }
}
