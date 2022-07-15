using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

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

    public void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);

    }
}
