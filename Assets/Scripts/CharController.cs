using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    Player player;
    GameObject touchingDoor;
    private Animator animator;
    private int idleState = Animator.StringToHash("Player_idle");
    public float baseSpeed = 10f;
    public float maxStamina;
    public float stamina {get; set;}
    private bool canRun = true;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        touchingDoor = null;
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

        if (player.Health <= 0) {
            player.CurrentState = Player.player_states.died;
            SceneManager.LoadScene(6); //death screen
        }

        if (player.CurrentState == Player.player_states.survived)
            SceneManager.LoadScene(5); //survived screen

        if (touchingDoor != null) {
            if (Input.GetKeyDown("E")) {
                openDoor(touchingDoor);
            }
        }
    }

    private void openDoor(GameObject door) {

    }

    private bool isMoving() {
        return (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
    }

    IEnumerator runCooldown() {
        canRun = false;
        yield return new WaitForSecondsRealtime(5f);
        canRun = true;
    }
    
    public void OnCollisionEnter2D(Collision2D col) {
        switch (col.gameObject.tag) {
            case "Health":
                player.Health = player.Health + ((player.Health <= 75) ? 25 : (100 - player.Health)); //adds 25 health to player but doesn't go over 100
                col.gameObject.SetActive(false); //deactivates the health pickup so that it can't be used more than once
                break;
            case "Key":
                player.KeyState = Player.player_states.hasKey; //change player state to have key
                col.gameObject.SetActive(false); //deactivates the key so it can only be picked up once
                break;
            case "Door":
                touchingDoor = col.gameObject;
                break;
        }
    }

    public void OnCollisionExit2D(Collision2D col) {
        switch (col.gameObject.tag) {
            case "Door":
                touchingDoor = null;
                break;
        }
    }

}
