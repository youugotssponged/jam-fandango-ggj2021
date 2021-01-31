using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    public GameObject[] last2Doors = new GameObject[2];
    public GameObject[] bossObjects;
    public Timer timer;
    public PauseMenuController pmc;
    public PlayerHUDController hud;
    private AudioSource audioSource;
    private AudioSource walkingAudio;
    private AudioSource musicAudio;
    public AudioClip[] playerSounds; //0 should be door opening, 1 is health pickup, 2 is hurt noise, 3 is walking, 4 is running
    public AudioClip[] musicSounds; //0 should be ambient, 1 is chase music, 2 is boss music
    Player player;
    GameObject touchingDoor;
    private Animator animator;
    private int idleState = Animator.StringToHash("Player_idle");
    public float baseSpeed = 10f;
    public float maxStamina;
    public float stamina {get; set;}
    private bool canMove = true;
    private bool canRun = true;
    private bool canTakeDmg = true;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sources= GetComponents<AudioSource>();
        audioSource = sources[0];
        audioSource.volume = 0.65f;
        audioSource.loop = false;
        walkingAudio = sources[1];
        walkingAudio.volume = 0.45f;
        walkingAudio.loop = true;
        walkingAudio.clip = playerSounds[3]; //walking
        walkingAudio.Play();
        musicAudio = sources[2];
        musicAudio.volume = 0.4f;
        musicAudio.loop = true;
        musicAudio.clip = musicSounds[0];
        musicAudio.Play();
        touchingDoor = null;
        player = new Player();
        stamina = maxStamina;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        timer.TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pmc.isPaused) {
            if (player.CurrentState == Player.player_states.survived) {
                timer.Stop();
                SceneManager.LoadScene(5); //survived screen
            }
            if (player.Health <= 0) {
                player.CurrentState = Player.player_states.died;
                SceneManager.LoadScene(6); //death screen
            }

            float horizMove = Input.GetAxis("Horizontal");
            float vertMove = Input.GetAxis("Vertical");

            bool running = (canRun && isMoving()) ? Input.GetKey("left shift") : false;
            float speed = (running) ? baseSpeed*1.5f : baseSpeed;
            speed = (isMoving()) ? speed : 0;
            walkingAudio.mute = !isMoving();
            walkingAudio.clip = (running) ? playerSounds[4] : playerSounds[3];
            if (!walkingAudio.isPlaying)
                walkingAudio.Play();

            hud.HPBarImage.fillAmount = (player.Health * 5f) / 500;
            hud.STBarImage.fillAmount = (stamina / 2) / 500;

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

            Vector3 moveVector = new Vector3(horizMove * speed, 0, vertMove * speed);
            if (canMove) {
                rb.velocity = moveVector;
            } else {
                rb.velocity = new Vector3(0, 0, 0);
            }
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
            animator.SetFloat("running", (running) ? 2 : 1);
            animator.SetBool("right", (horizMove > 0));
            animator.SetInteger("health", player.Health);

            if (touchingDoor != null) {
                if (Input.GetKeyDown(KeyCode.E) && player.KeyState == Player.player_states.hasKey) {
                    openDoor(touchingDoor);
                }
            }
        } else {
            walkingAudio.mute = true;
        }
    }

    private void openDoor(GameObject door) {
        Animator anim = door.GetComponentInParent<Animator>();
        anim.SetTrigger("open");
        playSound("door");
        player.KeyState = Player.player_states.noKey;
        hud.keyFound = false;
    }

    private bool isMoving() {
        return (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
    }

    IEnumerator runCooldown() {
        canRun = false;
        yield return new WaitForSecondsRealtime(5f);
        canRun = true;
    }
    
    public void OnCollisionEnter(Collision col) {
        switch (col.gameObject.tag) {
            case "Health":
                if (player.Health < 100) {
                    player.Health = player.Health + ((player.Health <= 70) ? 30 : (100 - player.Health)); //adds 30 health to player but doesn't go over 100
                    col.gameObject.SetActive(false); //deactivates the health pickup so that it can't be used more than once
                    playSound("health");
                }
                break;
            case "Key":
                player.KeyState = Player.player_states.hasKey; //change player state to have key
                hud.keyFound = true;
                col.gameObject.SetActive(false); //deactivates the key so it can only be picked up once
                break;
            case "Enemy":
                if (canTakeDmg) {
                    takeDamage(15);
                    StartCoroutine("dmgCooldown");
                }
                break;
        }
    }
    
    public void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Door")
            touchingDoor = col.gameObject;
        if (col.gameObject.tag == "Final door")
            player.CurrentState = Player.player_states.survived;
        if (col.gameObject.name == "BossTrigger") {
            col.gameObject.SetActive(false);
            StartCoroutine("startBossEncounter");
        }
    }

    IEnumerator startBossEncounter() {
        playMusic("boss");
        canMove = false;
        foreach (GameObject obj in bossObjects) {
            obj.SetActive(true);
            obj.GetComponentInChildren<Animator>().SetBool("transform", true);
        }
        Camera cam = GetComponentInChildren<Camera>();
        float zoom = cam.orthographicSize;
        cam.orthographicSize *= 2.5f;
        yield return new WaitForSecondsRealtime(3f);
        cam.orthographicSize = zoom;
        canMove = true;
        foreach (GameObject obj in bossObjects) {
            obj.transform.LookAt(transform);
            obj.GetComponent<Enemy>().enabled = true;
            obj.GetComponent<EnemyFieldOfView>().enabled = true;
        }
        yield return new WaitForSecondsRealtime(10f);
        last2Doors[0].GetComponentInChildren<Animator>().SetTrigger("open");
        last2Doors[1].GetComponentInChildren<Animator>().SetTrigger("open");
    }

    public void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Door")
            touchingDoor = null;
    }

    public void takeDamage(int dmg) {
        player.Health -= dmg;
        playSound("hurt");
    }

    IEnumerator dmgCooldown() {
        canTakeDmg = false;
        yield return new WaitForSecondsRealtime(5f);
        canTakeDmg = true;
    }

    private void playSound(string sound) {
        switch (sound) {
            case "door":
                audioSource.clip = playerSounds[0];
                break;
            case "health":
                audioSource.clip = playerSounds[1];
                break;
            case "hurt":
                audioSource.clip = playerSounds[2];
                break;
        }
        audioSource.Play();
    }

    public void playMusic(string music) {
        switch (music) {
            case "ambient":
                if (!bossObjects[0].activeInHierarchy && musicAudio.clip != musicSounds[0])
                    musicAudio.clip = musicSounds[0];
                break;
            case "chase":
                if (!bossObjects[0].activeInHierarchy && musicAudio.clip != musicSounds[1])
                    musicAudio.clip = musicSounds[1];
                break;
            case "boss":
                if (musicAudio.clip != musicSounds[2]) {
                    musicAudio.volume = 0.2f;
                    musicAudio.time = 10;
                    musicAudio.clip = musicSounds[2];
                }
                break;
        }
        musicAudio.Play();
    }

}
