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
        timer += Time.deltaTime;

        if (!attacking)
        {
            transform.Translate(-speed, 0, 0);
        }
        else
        {
            //Bomb enemy stops moving when it detects a player in range. Blows up after the fuse time
            timer += Time.deltaTime;
            if (timer >= fuse)
            {
                //Will instantiate explosion
                Destroy(gameObject);
            }
        }
    }
}
