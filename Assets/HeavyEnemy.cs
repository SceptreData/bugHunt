using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy {

    void Start()
    {

    }

    //Update and Start functions aren't inherited, so this is in here
    void FixedUpdate()
    {
        if (!gameOver)
        {
            timer += Time.deltaTime;

            //Moves if not attacking, otherwise... attacks
            if (!attacking)
            {
                transform.Translate(-speed, 0, 0);
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= shotInterval)
                {
                    //Prevents all enemies from shooting at the same time
                    int rng = Random.Range(1, 101);
                    if (rng <= shotChance)
                    {
                        Bullet newBullet = Instantiate(shot, transform.position, Quaternion.identity);
                        newBullet.SetFacingRight(false);
                        timer = 0;
                    }
                }
            }

            //Air movement
            if (inAir)
            {
                transform.Translate(0, jumpPower, 0);

                int layerMask = 1 << 8;
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.55f, layerMask))
                {
                    //One last check to avoid getting stuck on walls
                    if (!Physics2D.Raycast(transform.position, Vector2.left, 0.4f, layerMask))
                    {
                        inAir = false;
                    }
                }
            }
        }
    }
}
