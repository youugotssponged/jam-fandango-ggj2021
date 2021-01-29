using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Animator animator;
    private int idleState;
    private int walkState;
    private int runState;
    public float baseSpeed = 10f;
    public float maxStamina;
    private float stamina;
    private bool canRun = true;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        idleState = Animator.StringToHash("Idle");
        walkState = Animator.StringToHash("Walk");
        runState = Animator.StringToHash("Run");
    }

    // Update is called once per frame
    void Update()
    {
        float horizMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        bool running = (canRun && isMoving()) ? Input.GetKey("left shift") : false;
        float speed = (running) ? baseSpeed*1.5f : baseSpeed;

        if (running) {
            if (stamina <= 0) {
                StartCoroutine("runCooldown");
            } else {
                stamina--;
            }
        } else {
            if (stamina < maxStamina)
                stamina += 0.25f;
        }

        Vector2 moveVector = new Vector2(horizMove * speed, vertMove * speed);
        rb.velocity = moveVector;
        if (isMoving()) {
            if (running) {
                animator.SetTrigger(runState);
                //set player state to running
            } else {
                animator.SetTrigger(walkState);
                //set player state to walking
            }
        } else {
            animator.SetTrigger(idleState);
            //set player state to idle
        }
    }

    private bool isMoving() {
        return (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
    }

    IEnumerator runCooldown() {
        canRun = false;
        yield return new WaitForSecondsRealtime(5f);
        canRun = true;
    }
    
    public void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.name) {
            case "Key":
                //change player state to have key
                break;
            case "Door":
                break;
        }
    }

}
