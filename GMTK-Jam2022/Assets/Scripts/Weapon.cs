using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;
    public float force;
    public float fireRate = 2f;
    private float lastShot = 0;

    // Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        //position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

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
            GameObject spell = Instantiate(projectile, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
            spell.GetComponent<SpriteRenderer>().transform.rotation = firePoint.rotation;
            rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
            lastShot = Time.time;
        }
    }


}
