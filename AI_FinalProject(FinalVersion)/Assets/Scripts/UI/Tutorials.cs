using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour {

    GameManager manager;
    PlayerMovement playerM;
    float alpha;
    private Text textColor;

    public bool isWASD;
    public bool isSwordTutorial;
    public bool isNaviTutorial;
    public bool isDarkNaviTutorial;
    public bool isNewHighScore;
    bool goingUp = true;

    void Start () {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        textColor = GetComponent<Text>();
        alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (isSwordTutorial)
        {
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);

            if (playerM.grabbedSword)
            {
                if (goingUp)
                {
                    alpha += Time.deltaTime * 0.3f;
                    if (alpha >= 1)
                    {
                        goingUp = false;
                    }
                }
                if (!goingUp)
                {
                    StartCoroutine(ShowTutorial());
                }
            }
        }

        else if(isWASD)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g,
                                                             GetComponent<SpriteRenderer>().color.b, alpha);
            if(goingUp)
            {
                alpha += Time.deltaTime * 0.3f; 
                if(alpha >= 1)
                {
                    goingUp = false;
                }
            }
            if (!goingUp)
            {
                StartCoroutine(ShowTutorial());
            }
        }

        if (isNaviTutorial)
        {
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);

            if (manager.hasNavi)
            {
                if (goingUp)
                {
                    alpha += Time.deltaTime * 0.3f;
                    if (alpha >= 1)
                    {
                        goingUp = false;
                    }
                }
                if (!goingUp)
                {
                    StartCoroutine(ShowTutorial());
                }
            }
        }
        if (isDarkNaviTutorial)
        {
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);

            if (manager.hasDarkNavi)
            {
                if (goingUp)
                {
                    alpha += Time.deltaTime * 0.3f;
                    if (alpha >= 1)
                    {
                        goingUp = false;
                    }
                }
                if (!goingUp)
                {
                    StartCoroutine(ShowTutorial());
                }
            }
        }

        if (isNewHighScore)
        {
            textColor.color = new Color(textColor.color.r, textColor.color.g, textColor.color.b, alpha);

            if (goingUp && (manager.actualScore > Singleton.Instance.highestScore))
            {
                alpha += Time.deltaTime * 0.5f;
                if(alpha >= 1)
                {
                    goingUp = false;
                }
            }
            if (!goingUp)
            {
                alpha -= Time.deltaTime * 0.3f;
                if (alpha <= 0)
                {
                    alpha = 0;
                }
            }
        }
    }

    IEnumerator ShowTutorial()
    {
        yield return new WaitForSeconds(2);
        alpha -= Time.deltaTime * 0.3f;

        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
