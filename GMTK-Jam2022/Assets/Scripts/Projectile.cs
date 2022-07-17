using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D projectRigid;
    private Vector3 shootDirection;
    AudioSource hitEffect;
    public AudioClip clip;
    void Start()
    {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        hitEffect = GetComponent<AudioSource>();
        //sr.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position += shootDirection * speed * Time.deltaTime;
    }





    private void OnCollisionEnter2D(Collision2D collision)
    {

        AudioSource.PlayClipAtPoint(clip, collision.gameObject.transform.position, 1f);

        if (collision.gameObject.CompareTag("Monster"))
        {

            collision.gameObject.GetComponent<MobMonsterController>().Death();


        }
        if (collision.gameObject.CompareTag("MProj"))
        {

            collision.gameObject.GetComponent<MobProjectile>().HitByPlProj();


        }
        if (collision.gameObject.CompareTag("SpMonster"))
        {

            collision.gameObject.GetComponent<MobMonsterSpecial>().Damage();

        }
        Destroy(gameObject);
    }

}

