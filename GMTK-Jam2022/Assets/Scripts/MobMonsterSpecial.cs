using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class MobProjectile : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D mbProjectile;
    public float speed;
=======
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
>>>>>>> will

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
<<<<<<< HEAD
        mbProjectile = GetComponent<Rigidbody2D>();
=======
        monRigidbody = GetComponent<Rigidbody2D>();
>>>>>>> will
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector2 playerPosition = player.GetComponent<PlayerCharacter>().GetPosition();
        mbProjectile.position = Vector2.MoveTowards(mbProjectile.position, playerPosition, speed * Time.deltaTime);
=======
        SpMobAI();
    }

    void SpMobAI()
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


>>>>>>> will
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCharacter>().Death();
<<<<<<< HEAD
            Destroy(gameObject);
        }
    }

    public void HitByPlProj()
    {
        Destroy(gameObject);

    }

=======
        }
    }

    private IEnumerator FireRate(float WaitTime)
    {



        yield return new WaitForSeconds(WaitTime);
        canFire = true;

    }

    void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {

            Instantiate(projectile, monRigidbody.position, Quaternion.identity);
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
>>>>>>> will
}