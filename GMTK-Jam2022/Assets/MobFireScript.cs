using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobFireScript : MonoBehaviour
{
    private Rigidbody2D monRigidbody;
    public float fireRate = 5f;
    private float lastShot = 0f;
    public AudioSource fireSpell;
    public GameObject projectile;
    bool canFire;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        monRigidbody = GetComponentInParent<Rigidbody2D>();
        canFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {


        if (Time.time > fireRate + lastShot)
        {

            //Vector2 spawnPos = new Vector2(monRigidbody.position.x, monRigidbody.position.y - 1f);
            Vector2 spawnPos = new Vector2(_transform.position.x, _transform.position.y);
            fireSpell.Play();
            var spellMon = Instantiate(projectile, spawnPos, Quaternion.identity, gameObject.transform);

            lastShot = Time.time;
            //canFire = true;
        }

    }
}
