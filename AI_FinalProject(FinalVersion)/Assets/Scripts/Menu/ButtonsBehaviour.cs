using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour {

    [SerializeField]
    private RectTransform rtMainMenu;
    private Vector3 toLeftMM;
    private Vector3 toRightMM;

    [SerializeField]
    private RectTransform rtHowToPlayMenu;
    private Vector3 toLeftHM;
    private Vector3 toRightHM;

    [SerializeField]
    GameObject mainMenuRocks;

    [SerializeField]
    GameObject howToPlayRocks;

    public Text highestScoreText;

    private void Awake() {
        Singleton.Instance.Load();
    }

    void Start () {
        toRightMM = rtMainMenu.anchoredPosition;
        toLeftMM = new Vector3(-1280, 0);

        toLeftHM = rtHowToPlayMenu.anchoredPosition;
        toRightHM = new Vector3(1280, 0);
        rtHowToPlayMenu.anchoredPosition = new Vector3(1280, 0);

        if(Singleton.Instance.highestScore == 0)
        {
            highestScoreText.gameObject.SetActive(false);
        }
        else
        {
            highestScoreText.gameObject.SetActive(true);
            highestScoreText.text = "" + Singleton.Instance.highestScore;
        }
    }
	
	void Update () {
		
	}

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Exit()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void HowToPlay()
    {
        mainMenuRocks.SetActive(false);
        howToPlayRocks.SetActive(true);
        StartCoroutine(MoveMenu(rtMainMenu,toLeftMM));
        StartCoroutine(MoveMenu(rtHowToPlayMenu, toLeftHM));
    }

    public void Back()
    {
        mainMenuRocks.SetActive(true);
        howToPlayRocks.SetActive(false);
        StartCoroutine(MoveMenu(rtMainMenu, toRightMM));
        StartCoroutine(MoveMenu(rtHowToPlayMenu, toRightHM));
    }

    IEnumerator MoveMenu(RectTransform start,Vector3 target)
    {
        float currentTime = 0;
        float duration = 0.5f;

        Vector3 origin = start.anchoredPosition;

        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / duration);

            float smooth = percent * percent * (3f - 2f * percent);
            start.anchoredPosition = Vector3.Lerp(origin, target, smooth);
            yield return null;
        }
    }
}
