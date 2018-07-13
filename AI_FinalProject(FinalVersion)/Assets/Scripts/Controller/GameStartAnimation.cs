using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartAnimation : MonoBehaviour {

    public GameObject ganon;
    public GameObject sword;
    bool isMoving = true;
    public GameObject rockBlock;

    void Awake () {

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>().enabled = false;
        GameObject.Find("SwordItem").GetComponent<SwordItemBehav>().enabled = false;
        GetComponent<GameManager>().enabled = false;
        GameObject.Find("Canvas").SetActive(false);
        
    }

    private void Start()
    {
        
    }

    void Update () {

       StartCoroutine(StartCutscene());
        
    }

    IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(0.4f);
        rockBlock.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        ganon.transform.position = Vector3.MoveTowards(ganon.transform.position, sword.transform.position, Time.deltaTime * 0.85f);

        yield return new WaitForSeconds(2.9f);


        sword.transform.parent = ganon.transform;
        ganon.transform.position = Vector3.MoveTowards(ganon.transform.position, GameObject.Find("CameraEntranceAnimation").transform.position, Time.deltaTime * 3f);
        


        yield return new WaitForSeconds(4);
        isMoving = false;
        GameObject.Find("WASD").GetComponent<Tutorials>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find("SwordItem").GetComponent<SwordItemBehav>().enabled = true;
        Destroy(ganon);
        Destroy(this);
    }
}
