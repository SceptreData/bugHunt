using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy {

    [SerializeField]
    float fuse = 0;

	void Start () {
		
	}

    void FixedUpdate()
    {
        if (!gameOver)
        {
            timer += Time.deltaTime;

            if (!attacking)
            {
                transform.Translate(-speed, 0, 0);
            } else
            {
                //Bomb enemy stops moving when it detects a player in range. Blows up after the fuse time
                timer += Time.deltaTime;
                if (timer >= fuse)
                {
                    //Will instantiate explosion
                    Destroy(gameObject);
                }
            }

            //Air movement
            if (inAir)
            {
                transform.Translate(0, jumpPower, 0);

                int layerMask = 1 << 8;
                if (Physics2D.Raycast(transform.position, Vector2.down, 0.251f, layerMask))
                {
                    //One last check to avoid getting stuck on walls
                    if (!Physics2D.Raycast(transform.position, Vector2.left, 1.1f, layerMask))
                    {
                        inAir = false;
                    }
                }
            }
        }
    }
}
