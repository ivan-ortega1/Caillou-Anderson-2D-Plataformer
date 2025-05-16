using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform enemyTransform;
    public AudioSource enemyAud;
    public AudioSource stompAud;
    public AudioClip stomp;
    public GameObject explosionEffect;
    public Collider2D enemyCol;
    public Collider2D headCol;
    public GameObject enemy;
    public PlayerMovement player;
    public GameObject playerObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D _col)
    {
        if(_col.gameObject.tag == "Player")
        {
            Stomped();
        }
    }

    void Stomped()
    {
        enemy.tag = "Untagged";
        enemyAud.Stop();
        stompAud.clip = stomp;
        stompAud.Play();
        enemyTransform.localScale = new Vector3(0.7f, 0.2f, 0.7f);
        StartCoroutine(ExplodeWait());
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator ExplodeWait(){
        yield return new WaitForSeconds(1);
        Explode();
    }
    void Explode(){
        player.kills++;
        var lastExplosionEffect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(enemy);
        Destroy(lastExplosionEffect, 4.0f);
    }
}
