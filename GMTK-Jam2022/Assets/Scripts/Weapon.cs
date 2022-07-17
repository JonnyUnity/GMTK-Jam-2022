using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint, parent;
    public GameObject projectile;
    public float force;
    public float fireRate = 2f;
    private float lastShot = 0;
    public AudioSource fire;

    // Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<PlayerCharacter>().transform;
        //position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //GetComponentInChildren<Weapon>().Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time > fireRate + lastShot)
        {
            GameObject spell = Instantiate(projectile, firePoint.position, firePoint.rotation, parent);
            //spell.transform.parent = gameObject.transform;
            Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
            spell.GetComponent<SpriteRenderer>().transform.rotation = firePoint.rotation;
            fire.Play();
            rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
            lastShot = Time.time;
        }
    }


}
