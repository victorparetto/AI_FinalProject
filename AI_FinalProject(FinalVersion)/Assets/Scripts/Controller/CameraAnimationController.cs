using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationController : MonoBehaviour {

    bool isMoving;
    Camera mainCamera;
    public GameObject blockRock;

    public GameObject canvasObject;

	void Awake () {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>().enabled = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().enabled = false;
    }
	
	void Update () {

        if (isMoving)
        {
            mainCamera.gameObject.transform.position = Vector3.MoveTowards(mainCamera.gameObject.transform.position, new Vector3(0, 0, -10), Time.deltaTime * 4);
            mainCamera.orthographicSize += Time.deltaTime * 0.7f;

            if(mainCamera.orthographicSize >= 5)
            {
                mainCamera.orthographicSize = 5;
                isMoving = false;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(MovingCamera());
        }
    }

    IEnumerator MovingCamera()
    {
        isMoving = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isWalkingUp", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isWalkingDown", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isWalkingLeft", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isWalkingRight", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("isIdle", true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(7.89f, -4.11f);
        GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>().enabled = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().enabled = false;

        canvasObject.SetActive(false);
        yield return new WaitForSeconds(3.4f);

        blockRock.SetActive(true);
        canvasObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find("EnemySpawner").GetComponent<SpawnerBehaviour>().enabled = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().enabled = true;
        Destroy(gameObject);
    }
}
