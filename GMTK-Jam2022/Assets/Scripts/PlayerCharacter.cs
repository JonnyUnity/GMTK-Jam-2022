using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public float speed;
    public GameObject projectile;
    public float force;
    public SpriteRenderer playerMod;
    public AudioSource walk;
    bool isMoving;


    [SerializeField] private EventChannelSO _playerDiedChannelSO;


    [Header("Audio")]
    [SerializeField] private AudioClip _deathSoundClip;


    private void Awake()
    {
        playerMod = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //walk.Play();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move()
    {

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //Vector2 moveDirection = new Vector2(horizontalAxis, verticalAxis);
        Vector2 moveDirection = new Vector2(horizontalAxis, verticalAxis);

        //playerBody.AddForce(moveDirection * speed);
        playerBody.MovePosition(playerBody.position + moveDirection * speed * Time.fixedDeltaTime);

        //playerBody.position = Vector2.MoveTowards(playerBody.position, moveDirection, speed * Time.deltaTime);


        //walk.Play();


        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) { isMoving = true; } else { isMoving = false; }
        if (isMoving && !walk.isPlaying) { walk.Play(); }
        if (!isMoving) { walk.Stop(); }

        if (Input.anyKeyDown == false) { Stop(); }




    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.PLAYER_DEAD)
            return;

        Move();
        RotateWep();

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

        if (angle > 130 && angle < 179)
        {
            playerMod.flipX = false;
        }
        else if (angle > -179 && angle < -130)
        {
            playerMod.flipX = false;
        }
        else
        {
            playerMod.flipX = true;
        }

        pivot.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);


    }


    public Vector2 GetPosition()
    {
        return playerBody.position;
    }

    public void Death()
    {
        if (GameManager.Instance.State == GameState.PLAYER_DEAD)
            return;

        AudioManager.Instance.FadeMusicOut(0.1f);
        walk.PlayOneShot(_deathSoundClip);

        GameManager.Instance.PlayerHasDied();
        _playerDiedChannelSO.RaiseEvent();
    }

}
