using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    enum playerState
    {
        GoingUp,
        GoingDown,
        GoingRight,
        GoingLeft,
        IDLE
    }

    playerState state;
    public bool canMove;

    Rigidbody2D rbd2;
    Animator anim;

    public float speed = 3;


    public int lifes = 3;
    public bool isInvincible = false;
    public GameObject stock1;
    public GameObject stock2;
    public GameObject stock3;
    public Color[] colors;

    public static List<Vector3> path = new List<Vector3>();
    public int nearestNodIndex = 0;

    public bool grabbedSword;

    GameManager manager;

    void Start()
    {
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

        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        rbd2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = playerState.IDLE;
        canMove = true;
        
    }

    void Update()
    {
        CheckDistanceToNod();

        float posX = Input.GetAxisRaw("Horizontal");
        float posY = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(posX, posY, 0) * Time.deltaTime;

        if (canMove)
        {
            // switch con todos los estados
            switch (state)
            {
                // cada estado tiene
                // - acción a ejecutar
                // - condicioes de salida

                case playerState.GoingUp:
                    {
                        // acción:
                        if (Input.GetKey(KeyCode.W))
                        {
                            rbd2.velocity = new Vector2(0, speed);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttacking", false);
                            anim.SetBool("isWalkingDown", false);
                            anim.SetBool("isWalkingRight", false);
                            anim.SetBool("isWalkingLeft", false);
                            anim.SetBool("isWalkingUp", true);

                            if (grabbedSword)
                            {
                                if (Input.GetKeyDown(KeyCode.J))
                                {
                                    anim.SetBool("isWalkingUp", false);
                                    anim.SetBool("isAttacking", true);
                                }
                            }
                        }

                        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                        {
                            state = playerState.GoingDown;
                        }
                        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
                        {
                            state = playerState.GoingRight;
                        }
                        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
                        {
                            state = playerState.GoingLeft;
                        }
                        if (!Input.anyKey)
                        {
                            state = playerState.IDLE;
                        }

                        break;
                    }

                case playerState.GoingDown:
                    {
                        if (Input.GetKey(KeyCode.S))
                        {
                            rbd2.velocity = new Vector2(0, -speed);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttacking", false);
                            anim.SetBool("isWalkingUp", false);
                            anim.SetBool("isWalkingRight", false);
                            anim.SetBool("isWalkingLeft", false);
                            anim.SetBool("isWalkingDown", true);

                            if (grabbedSword)
                            {
                                if (Input.GetKeyDown(KeyCode.J))
                                {
                                    anim.SetBool("isWalkingDown", false);
                                    anim.SetBool("isAttacking", true);
                                }
                            }
                        }

                        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                        {
                            state = playerState.GoingUp;
                        }
                        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S))
                        {
                            state = playerState.GoingRight;
                        }
                        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S))
                        {
                            state = playerState.GoingLeft;
                        }
                        if (!Input.anyKey)
                        {
                            state = playerState.IDLE;
                        }

                        break;
                    }

                case playerState.GoingRight:
                    {
                        if (Input.GetKey(KeyCode.D))
                        {
                            rbd2.velocity = new Vector2(speed, 0);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttacking", false);
                            anim.SetBool("isWalkingUp", false);
                            anim.SetBool("isWalkingDown", false);
                            anim.SetBool("isWalkingLeft", false);
                            anim.SetBool("isWalkingRight", true);

                            if (grabbedSword)
                            {
                                if (Input.GetKeyDown(KeyCode.J))
                                {
                                    anim.SetBool("isWalkingRight", false);
                                    anim.SetBool("isAttacking", true);
                                }
                            }
                        }

                        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
                        {
                            state = playerState.GoingUp;
                        }
                        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
                        {
                            state = playerState.GoingDown;
                        }
                        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                        {
                            state = playerState.GoingLeft;
                        }
                        if (!Input.anyKey)
                        {
                            state = playerState.IDLE;
                        }

                        break;
                    }

                case playerState.GoingLeft:
                    {
                        if (Input.GetKey(KeyCode.A))
                        {
                            rbd2.velocity = new Vector2(-speed, 0);
                            anim.SetBool("isIdle", false);
                            anim.SetBool("isAttacking", false);
                            anim.SetBool("isWalkingUp", false);
                            anim.SetBool("isWalkingRight", false);
                            anim.SetBool("isWalkingDown", false);
                            anim.SetBool("isWalkingLeft", true);

                            if (grabbedSword)
                            {
                                if (Input.GetKeyDown(KeyCode.J))
                                {
                                    anim.SetBool("isWalkingLeft", false);
                                    anim.SetBool("isAttacking", true);
                                }
                            }
                        }

                        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A))
                        {
                            state = playerState.GoingUp;
                        }
                        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A))
                        {
                            state = playerState.GoingDown;
                        }
                        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
                        {
                            state = playerState.GoingRight;
                        }
                        if (!Input.anyKey)
                        {
                            state = playerState.IDLE;
                        }

                        break;
                    }

                case playerState.IDLE:
                    {
                        if (!Input.anyKey)
                        {
                            anim.SetBool("isWalkingUp", false);
                            anim.SetBool("isWalkingDown", false);
                            anim.SetBool("isWalkingLeft", false);
                            anim.SetBool("isWalkingRight", false);
                            anim.SetBool("isIdle", true);
                        }

                        if (Input.GetKey(KeyCode.W))
                        {
                            state = playerState.GoingUp;
                        }
                        if (Input.GetKey(KeyCode.S))
                        {
                            state = playerState.GoingDown;
                        }
                        if (Input.GetKey(KeyCode.D))
                        {
                            state = playerState.GoingRight;
                        }
                        if (Input.GetKey(KeyCode.A))
                        {
                            state = playerState.GoingLeft;
                        }

                        break;
                    }
            }
        }

        if(lifes > 3)
        {
            lifes = 3;
        }

        if(lifes == 3)
        {
            stock1.SetActive(true);
            stock2.SetActive(true);
            stock3.SetActive(true);
        }

        if (lifes == 2)
        {
            stock1.SetActive(true);
            stock2.SetActive(true);
            stock3.SetActive(false);
        }

        if (lifes == 1)
        {
            stock1.SetActive(true);
            stock2.SetActive(false);
            stock3.SetActive(false);
        }

        if (lifes == 0)
        {
            stock1.SetActive(false);
            stock2.SetActive(false);
            stock3.SetActive(false);

            StartCoroutine(DeathAnimation());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyBullet" && !isInvincible)
        {
            StartCoroutine(Invincible());
            Destroy(other.gameObject);
        }

        if(other.tag == "SwordItem")
        {
            other.GetComponent<SwordItemBehav>().TeleportSword();
            other.gameObject.SetActive(false);
            grabbedSword = true;
            manager.timerSwordLeft = 30;    
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !isInvincible)
        {
            StartCoroutine(Invincible());
        }

        if (other.tag == "Ghost" && !isInvincible)
        {
            StartCoroutine(Invincible());
        }
    }

    public IEnumerator Invincible()
    {
        lifes -= 1;
        isInvincible = true;

        GetComponent<SpriteRenderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().material.color = colors[0];

        isInvincible = false;

    }

    IEnumerator DeathAnimation()
    {
        manager.enabled = false;
        anim.SetBool("isWalkingUp", false);
        anim.SetBool("isWalkingDown", false);
        anim.SetBool("isWalkingLeft", false);
        anim.SetBool("isWalkingRight", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isIdle", true);
        canMove = false;
        transform.Rotate(0, 0, -10);

        yield return new WaitForSeconds(1.5f);

        Singleton.Instance.latestScore = manager.actualScore;

        if(Singleton.Instance.highestScore < manager.actualScore)
        {
            Singleton.Instance.highestScore = manager.actualScore;
        }

        Singleton.Instance.Save();

        SceneManager.LoadScene(2);
    }

    void CheckDistanceToNod()
    {
        float shorterDistanceToNod = 9999;

        for (int i = 0; i < path.Count; i++)
        {        
            if (Vector3.Distance(transform.position, path[i]) < shorterDistanceToNod)
            {
                shorterDistanceToNod = Vector3.Distance(transform.position, path[i]);
                nearestNodIndex = i;
            }


        }
    }
}
