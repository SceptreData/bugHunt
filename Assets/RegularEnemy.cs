using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : Enemy {

	void Start () {
		
	}
	
    //Update and Start functions aren't inherited, so this is in here
	void FixedUpdate () {
        timer += Time.deltaTime;

        //Moves if not attacking, otherwise... attacks
        if (!attacking)
        {
            transform.Translate(-speed, 0, 0);
        } else
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
    }
}
