using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMonsterSpecial : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Rigidbody2D monRigidbody;


    public float health = 4f;
    private int score;
    public AudioSource floatingHum;


    [SerializeField] private GOEventChannelSO _removeObjectChannelSO;
    [SerializeField] private int _score;

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
        if (GameManager.Instance.State == GameState.PLAYER_DEAD)
        {
            monRigidbody.velocity = Vector2.zero;
            floatingHum.Stop();
            return;
        }


        //if (player == null)
        //{
        //    monRigidbody.velocity = Vector2.zero;
        //    floatingHum.Stop();
        //}
        //else
        //{

            Vector2 playerPosition = player.GetComponent<PlayerCharacter>().GetPosition();
            monRigidbody.velocity = Vector2.zero;

            if (Vector2.Distance(player.GetComponent<PlayerCharacter>().GetPosition(), monRigidbody.position) >= 0.5)
            {
                monRigidbody.velocity = Vector2.zero;
                Vector2 abovePl = new Vector2(playerPosition.x, playerPosition.y + 2.5f);
                monRigidbody.position = Vector2.MoveTowards(monRigidbody.position, abovePl, speed * Time.deltaTime);

            }
            else
            {
                monRigidbody.velocity = Vector2.zero;




            }
        //}



    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerCharacter>().Death();
        }
    }





    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            _removeObjectChannelSO.RaiseEvent(gameObject, _score);
            //Destroy(gameObject);

        }


    }
}
