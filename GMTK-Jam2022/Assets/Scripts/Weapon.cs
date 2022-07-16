using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;
    public float force;


    // Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
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

        GameObject spell = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = spell.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);


    }
}
