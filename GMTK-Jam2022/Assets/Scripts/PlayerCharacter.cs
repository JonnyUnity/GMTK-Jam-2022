using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateWep();
    }

    void Move()
    {

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //Vector2 moveDirection = new Vector2(horizontalAxis, verticalAxis);
        Vector3 moveDirection = new Vector3(horizontalAxis, verticalAxis, 0);

        playerBody.AddForce(moveDirection * speed);

        if (Input.anyKeyDown == false)
        {
            Stop();

        }

        if (Input.GetMouseButtonDown(0))
        {
            GetComponentInChildren<Weapon>().Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void Stop()
    {
        playerBody.velocity = Vector2.zero;

    }

    void RotateWep()
    {
        Pivot pivot = gameObject.GetComponentInChildren(typeof(Pivot)) as Pivot;
        float angle;
        Vector3 pointPosition = (Input.mousePosition);
        pointPosition = Camera.main.ScreenToWorldPoint(pointPosition);
        //pointPosition.z = -(transform.position.x - Camera.main.transform.position.x);



        Vector3 target = pointPosition - transform.position;

        target.Normalize();

        angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        pivot.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        //Debug.Log(pivot.transform.rotation);


    }


    public Vector2 GetPosition()
    {
        return playerBody.position;
    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
