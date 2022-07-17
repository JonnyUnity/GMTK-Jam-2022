using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMonsterController : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Rigidbody2D monRigidbody;
    public SpriteRenderer monSprite;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAI();
    }

    void MonsterAI()
    {
        Vector2 playerPosition = player.GetComponent<PlayerCharacter>().GetPosition();
        monRigidbody.velocity = Vector2.zero;
        monRigidbody.position = Vector2.MoveTowards(monRigidbody.position, playerPosition, speed * Time.deltaTime);
        float angle;
        Vector3 playerLook = playerPosition;

        Vector3 target = playerLook - transform.position;
        target.Normalize();
        angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        if (angle > 130 && angle < 179)
        {
            monSprite.flipX = false;
        }
        else if (angle > -179 && angle < -130)
        {
            monSprite.flipX = false;
        }
        else
        {
            monSprite.flipX = true;
        }
        monRigidbody.transform.rotation = Quaternion.Euler(monRigidbody.transform.rotation.x, monRigidbody.transform.rotation.y, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCharacter>().Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
