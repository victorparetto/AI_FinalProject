using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour {

    [SerializeField]
    Text latestScore;
    [SerializeField]
    Text highestScore;

	void Start () {
        Singleton.Instance.Load();

        latestScore.text = "" + Singleton.Instance.latestScore;
        highestScore.text = "" + Singleton.Instance.highestScore;
	}

	void Update () {
		
	}
}
