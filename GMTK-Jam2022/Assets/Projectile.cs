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

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += shootDirection * speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 shootDir)
    {
        shootDirection = shootDir;

    }
}
