using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public Rigidbody rb;
    public Camera playerCamera;
    public Animator animator;
    public AudioSource gameMusic;

    AudioSource audioSource;

    public LevelManager levelManager;

    public float speed;

    private Vector3 direction = Vector3.zero;

    public bool onTheGround;

    public GameOverScreen gameOverScreen;

    public bool fallFlat;
    public bool isFall = false;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

        trackingPlayer();
        inputKey();
        fallCharacterControl();

    }

    private void FixedUpdate()
    {
        moveChracter();
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Map"))
        {

            onTheGround = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Map"))
        {

            onTheGround = false;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Traffic"))
        {

            animator.SetTrigger("FallFlat");

            rb.velocity = Vector3.zero;

            rb.isKinematic = false;
            rb.useGravity = true;
            onTheGround = false;
            fallFlat = true;

            audioSource.Play();
            gameMusic.Stop();

            StartCoroutine(gameOver());


        }

        if (collision.gameObject.CompareTag("Traffic2"))
        {

            animator.SetTrigger("FallFlat2");

            rb.velocity = Vector3.zero;

            rb.isKinematic = false;
            rb.useGravity = true;
            onTheGround = false;
            fallFlat = true;

            audioSource.Play();
            gameMusic.Stop();

            StartCoroutine(gameOver());

        }
    }

    private void inputKey()
    {

        if (!fallFlat)
        {

            direction = Vector3.forward;

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }


            if (transform.position.y > 1)
            {

                if (Input.GetKeyDown(KeyCode.Space) && onTheGround)
                {

                    animator.SetTrigger("Jump");

                    rb.AddForce(Vector3.up * 5, ForceMode.Impulse);

                }

            }

            direction = direction.normalized * speed;

        }
    }
    public void moveChracter()
    {

        if (!fallFlat)
        {

            Vector3 newVelocity = rb.velocity;

            newVelocity.z = speed;

            if (Input.GetKey(KeyCode.A))
            {

                newVelocity.x = -speed * 0.5f;

            }

            else if (Input.GetKey(KeyCode.D))
            {

                newVelocity.x = speed * 0.5f;

            }

            else
            {

                newVelocity.x = 0;

            }

            rb.velocity = newVelocity;

        }
    }

    private void fallCharacterControl()
    {

        if (transform.position.y < -1f)
        {

            animator.SetTrigger("Fall");

            isFall = true;

            gameMusic.Stop();

            gameOverScreen.Setup();

        }

        if (transform.position.y > 1.2f)
        {

            animator.SetTrigger("Return Run");

            isFall = false;

        }

    }

    private void trackingPlayer()
    {

        Vector3 trackCamera = new Vector3(0, 2f, -4);

        playerCamera.transform.position = transform.position + trackCamera;
        playerCamera.transform.LookAt(transform.position + Vector3.up * 1.25f);

    }

    IEnumerator gameOver()
    {

        yield return new WaitForSecondsRealtime(1.3f);

        Time.timeScale = 0f;

        gameOverScreen.Setup();

    }

}