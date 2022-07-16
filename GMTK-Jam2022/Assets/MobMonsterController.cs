using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMonsterController : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Rigidbody2D monRigidbody;
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCharacter>().Death();
        }
    }


}
