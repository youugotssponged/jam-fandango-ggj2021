using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Player player;
    private Animator animator;
    private int idleState = Animator.StringToHash("Player_idle");
    public float baseSpeed = 10f;
    public float maxStamina;
    private float stamina;
    private bool canRun = true;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
        stamina = maxStamina;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        bool running = (canRun && isMoving()) ? Input.GetKey("left shift") : false;
        float speed = (running) ? baseSpeed*1.5f : baseSpeed;
        speed = (isMoving()) ? speed : 0;

        if (running) {
            if (stamina <= 0) {
                StartCoroutine("runCooldown");
            } else {
                stamina--;
            }
        } else {
            if (stamina < maxStamina) {
                if (isMoving()) {
                    stamina += 0.25f;
                } else {
                    stamina += 0.5f;
                }
            }
        }

        Vector2 moveVector = new Vector2(horizMove * speed, vertMove * speed);
        rb.velocity = moveVector;
        if (isMoving()) {
            if (running) {
                player.CurrentState = Player.player_states.running; //set player state to running                
            } else {
                player.CurrentState = Player.player_states.walking; //set player state to walking
            }
        } else {
            player.CurrentState = Player.player_states.idle; //set player state to idle
        }

        animator.SetFloat("speed", speed);
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
                player.KeyState = Player.player_states.hasKey;
                //change player state to have key
                break;
            case "Door":
                break;
        }
    }

}
