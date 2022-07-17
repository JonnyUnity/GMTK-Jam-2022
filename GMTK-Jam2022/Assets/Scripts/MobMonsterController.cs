using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMonsterController : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Rigidbody2D monRigidbody;
    public SpriteRenderer monSprite;
    AudioSource walk;


    [SerializeField] private GOEventChannelSO _removeObjectChannelSO;
    [SerializeField] private int _score;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        monRigidbody = GetComponent<Rigidbody2D>();
        walk = GetComponent<AudioSource>();
        walk.Play();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAI();
    }

    void MonsterAI()
    {
        if (GameManager.Instance.State == GameState.PLAYER_DEAD)
        {
            monRigidbody.velocity = Vector2.zero;
            walk.Stop();
            return;
        }
            


        //if (player == null)
        //{
        //    monRigidbody.velocity = Vector2.zero;
        //    walk.Stop();
        //}
        //else
        //{
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

        //}




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
        //Destroy(gameObject);
        _removeObjectChannelSO.RaiseEvent(gameObject, _score);
    }
}
