using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed;
    public Transform player;
    public GameObject playerObj;
    public float jumpPower;
    private bool isJumping;
    public AudioSource playerAudio;
    public AudioSource playerAudio_gulp;
    public AudioClip jumpSound;
    public AudioClip gulpSound;
    public AudioClip gulpSpecialSound;
    public AudioSource FS;
    public int pizzasCollected = 0;
    public int lives;
    public GameObject jumpEffect;
    public Transform jumpEffectPosition;
    public GameObject explosionEffect;
    public AudioSource playerAudio_explosion;
    public AudioClip explosionSound;
    public Animator playerAnim;
    public AudioClip oofDead;
    public GameObject hitEffect;
    public Transform hitEffectTransform;
    public bool dead;
    public GameObject gameOverScreen;
    public int lastRecord;
    public int kills;
    public int lastRecordKills;
    public bool canTakeDamage;
    public Collider2D playerCollider;
    public SpriteRenderer playerSprite;
    public int flickerAmount;
    public float flickerDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pizzasCollected = 0; // When the game starts, the variable of integer number of pizzas the player collected will be first set to 0
        kills = 0; // When the game starts, the variable of integer number of enemies the player has stepped on an killed will be first set to 0
        dead = false; // When the game starts, obviously the player is still alive, so the boolean variable of dead will be initially set to false
        isJumping = false; // When the game starts, obviously the player is still on the ground, so the boolean variable of if the player is on the air after jumping will be initially set to false
        playerAudio.GetComponent<AudioSource>(); // When the game starts, the AudioSource variable of playerAudio will get the component of the GameObject this scriptis assign that's an AudioSource
        lives = 3; // When the game starts, the player will have 3 lives
        canTakeDamage = true; // When the game starts, the player hasn't been hit yet by an enemy, so this variable is set to true so he can take damage and have invincibility for a second after getting hit
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position += new Vector3(-playerSpeed, 0.0f);
        }    

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += new Vector3(playerSpeed, 0.0f);
        }    
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }    
        if(lives <= 0){
            Death();
            int lastPizzasRecord = PlayerPrefs.GetInt("LastPizzasRecord");
            if(PlayerPrefs.HasKey("LastPizzasRecord") == false){
                //No record saved
                PlayerPrefs.SetInt("LastPizzasRecord", pizzasCollected);
                print("NEW RECORD = " + pizzasCollected);
                lastRecord = lastPizzasRecord;
            }
            else{
                //New record saved
                if(lastPizzasRecord < pizzasCollected){
                    PlayerPrefs.SetInt("LastPizzasRecord", pizzasCollected);
                    print("NEW RECORD = " + pizzasCollected);
                    lastRecord = lastPizzasRecord;
                }
            }
            int lastKillsRecord = PlayerPrefs.GetInt("LastKillsRecord");
            if(PlayerPrefs.HasKey("LastKillsRecord") == false){
                //No record saved
                PlayerPrefs.SetInt("LastKillsRecord", kills);
                print("NEW KILL RECORD = " + kills);
                lastRecordKills = lastKillsRecord;
            }
            else{
                //New record saved
                if(lastKillsRecord < kills){
                    PlayerPrefs.SetInt("LastKillsRecord", kills);
                    print("NEW KILL RECORD = " + kills);
                    lastRecordKills = lastKillsRecord;
                }
            }
        }
    }

    private void Jump()
    {
        if(isJumping == false)
        {
            playerObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpPower));
            isJumping = true;
            playerAudio.clip = jumpSound;
            playerAudio.Play();
            var lastJumpEffect = GameObject.Instantiate(jumpEffect, jumpEffectPosition.position, transform.rotation);
            Destroy(lastJumpEffect, 2.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if(dead == false){
            if(_col.gameObject.tag == "Floor")
            {
                isJumping = false;
            }
            if(_col.gameObject.tag == "Enemy")
            {
                if (canTakeDamage == true)
                {
                    Destroy(_col.gameObject);
                    lives--;
                    print("you have " + lives + " lives left.");
                    if (lives > 0)
                    {
                        var lastHitEffect = GameObject.Instantiate(hitEffect, hitEffectTransform.position, transform.rotation);
                        Destroy(lastHitEffect, 2.0f);
                        StartCoroutine(hitFlicker());
                    }
                }
                else
                {
                    Physics2D.IgnoreCollision(_col.collider, playerCollider);
                }
            }
        }
    }

    IEnumerator hitFlicker()
    {
        canTakeDamage = false;
        for (int i = 0; i < flickerAmount; i++)
        {
            playerSprite.color = new Color(255f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flickerDuration);
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flickerDuration);
            playerSprite.color = new Color(128f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flickerDuration);
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(flickerDuration);
            canTakeDamage = true;
        }
    }

    void OnTriggerEnter2D(Collider2D _tri)
    {
        if(dead == false){
            if(_tri.gameObject.tag == "Pizza")
            {
                ++pizzasCollected;
                playerAudio_gulp.clip = gulpSound;
                playerAudio_gulp.Play();
                Destroy(_tri.gameObject);
            }
            if(_tri.gameObject.tag == "Pizza_special")
            {
                pizzasCollected += 10;
                playerAudio_gulp.clip = gulpSpecialSound;
                playerAudio_gulp.Play();
                Destroy(_tri.gameObject);
            }
        }
    }

    public void Death(){
        this.gameObject.tag = "Untagged";
        dead = true;
        var lastExplosionEffect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(lastExplosionEffect, 4.0f);
        playerAnim.SetBool("IsDead?", true);
        print("DEAD");
        StartCoroutine(GameOverScreen());
        PlayerMovement movementScript = GetComponent<PlayerMovement>();
        movementScript.enabled = false;
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(3);
        gameOverScreen.SetActive(true);
    }
}
