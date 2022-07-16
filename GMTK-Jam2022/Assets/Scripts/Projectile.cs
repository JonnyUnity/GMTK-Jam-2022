using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D projectRigid;
    private Vector3 shootDirection;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //sr.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<MobMonsterController>().Death();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("MProj"))
        {
            collision.gameObject.GetComponent<MobProjectile>().HitByPlProj();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("SpMonster"))
        {
            collision.gameObject.GetComponent<MobMonsterSpecial>().Damage();
            Destroy(gameObject);
        }
    }

}
