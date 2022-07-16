using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectile;



    // Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        //position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector3 cameraPoint)
    {

        var spell = Instantiate(projectile, firePoint.position, Quaternion.identity);
        Vector3 shootDir = cameraPoint - firePoint.position.normalized;
        spell.GetComponent<Projectile>().MoveTo(shootDir);
    }
}
