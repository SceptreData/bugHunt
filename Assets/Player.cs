using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //SerializeField keeps the variables private, but lets you edit them in the Unity editor
    [SerializeField]
    float speed = 0;
    [SerializeField]
    float jumpSpeed = 0;
    [SerializeField]
    float maxTimeInAir = 0;
    [SerializeField]
    Bullet playerShot = null;

    bool movingRight = true;
    bool inAir = false;
    float timeInAir = 0f;
    bool firing = false;

	void Start () {
		
	}
	
	void Update () {

        //Grabs horizontal control value. Theoretically works with controllers.
        float horValue = Input.GetAxis("Horizontal");

        //Moves in proper direction. Updates sprite based on direction
        if (horValue > 0)
        {
            transform.Translate(speed, 0, 0);
            movingRight = true;
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (horValue < 0)
        {
            transform.Translate(-speed, 0, 0);
            movingRight = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //Checks to see if the player is in the air to prevent air-jumping
        if (Input.GetAxis ("Vertical") > 0 && !inAir)
        {
            Jump();
        }

        //Fires one shot per press of button
        if (Input.GetAxis("Fire1") != 0 && !firing)
        {
            firing = true;
            Shoot();
        } else if (Input.GetAxis("Fire1") == 0)
        {
            firing = false;
        }
	}

    private void FixedUpdate()
    {
        //Air movement is in physics update to prevent stuttering
        if (inAir)
        {
            //Slows upward velocity based on max time in air and jump speed. Can change in the editor
            float airSpeed = jumpSpeed * (1 - (timeInAir / maxTimeInAir));
            if (airSpeed < 0f)
            {
                airSpeed = 0f;
            }

            transform.Translate(0, airSpeed, 0);
            timeInAir += Time.deltaTime;

            //Raycast to see if player is back on the ground
            int layerMask = 1 << 8;
            if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerMask))
            {
                inAir = false;
            }
        }
    }

    void Shoot()
    {
        //Instantiates a new shot, changes direction based on player facing
        Bullet newBullet = Instantiate(playerShot, transform.position, Quaternion.identity);
        newBullet.SetFacingRight(movingRight);
    }

    void Jump()
    {
        //Jumps
        inAir = true;
        timeInAir = 0f;
    }
}
