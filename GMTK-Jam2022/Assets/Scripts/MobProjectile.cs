using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobProjectile : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D mbProjectile;
    public float speed;
    public float projDecay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mbProjectile = GetComponent<Rigidbody2D>();
        projDecay = Time.time + 2f;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        ProjectileDecay();
    }

    void FollowPlayer()
    {

        float angle;
        Vector2 playerPosition = player.GetComponent<PlayerCharacter>().GetPosition();
        mbProjectile.position = Vector2.MoveTowards(mbProjectile.position, playerPosition, speed * Time.deltaTime);
        Vector3 playerLook = playerPosition;

        Vector3 target = playerLook - transform.position;
        target.Normalize();
        angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCharacter>().Death();
            Destroy(gameObject);
        }
    }

    public void HitByPlProj()
    {
        Destroy(gameObject);

    }

    void ProjectileDecay()
    {
        if (Time.time > projDecay)
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }

    }
}
