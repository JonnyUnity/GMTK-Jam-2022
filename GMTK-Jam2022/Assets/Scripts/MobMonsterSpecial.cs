using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMonsterSpecial : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Rigidbody2D monRigidbody;
    public GameObject projectile;
    bool canFire = true;
    public float fireRate = 5f;
    private float lastShot = 0;
    public float health = 4f;
    private int score;
    public AudioSource fireSpell, floatingHum;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monRigidbody = GetComponent<Rigidbody2D>();
        //fireSpell = GetComponent<AudioSource>();
        floatingHum.Play();
    }

    // Update is called once per frame
    void Update()
    {
        SpMobAI();
    }

    void SpMobAI()
    {
        if (player == null)
        {
            monRigidbody.velocity = Vector2.zero;
            floatingHum.Stop();
        }
        else
        {

            Vector2 playerPosition = player.GetComponent<PlayerCharacter>().GetPosition();
            monRigidbody.velocity = Vector2.zero;

            if (Vector2.Distance(player.GetComponent<PlayerCharacter>().GetPosition(), monRigidbody.position) >= 10)
            {
                monRigidbody.velocity = Vector2.zero;
                monRigidbody.position = Vector2.MoveTowards(monRigidbody.position, playerPosition, speed * Time.deltaTime);
                //Debug.Log("Moving");
            }
            else
            {
                monRigidbody.velocity = Vector2.zero;

                Fire();

                //StartCoroutine(FireRate(5f));

                // Debug.Log("Shooting");
            }
        }



    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCharacter>().Death();
        }
    }



    void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
            Vector2 spawnPos = new Vector2(monRigidbody.position.x, monRigidbody.position.y + 1f);
            fireSpell.Play();
            var spellMon = Instantiate(projectile, spawnPos, Quaternion.identity, gameObject.transform);
            //spellMon.transform.parent = gameObject.transform;
            lastShot = Time.time;
        }

    }

    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);

        }


    }
}
