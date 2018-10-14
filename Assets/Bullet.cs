using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed = 0;
    [SerializeField]
    int damage = 0;
    [SerializeField]
    BulletHitEffect hitEffect = null;
    [SerializeField]
    bool friendly = true;

    bool movingRight;

	void Start () {
		
	}
	
	void Update () {
        //Moves based on initial facing
        if (movingRight)
        {
            transform.Translate(speed, 0, 0);
        } else
        {
            transform.Translate(-speed, 0, 0);
        }
	}

    public void SetFacingRight (bool facing)
    {
        //Sets initial facing
        movingRight = facing;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //If bullet collides with ground, creates a smoke effect
        if (col.gameObject.layer == 8)
        {
            BulletHitEffect newEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            newEffect.GenerateMovement(movingRight);
            Destroy(gameObject);
        }

        //If bullet is fired from player and hits enemy, enemy takes damage, creates a smoke effect
        if (col.gameObject.layer == 10 && friendly)
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            BulletHitEffect newEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            newEffect.GenerateMovement(movingRight);
            Destroy(gameObject);
        }

        //If bullet is fired from enemy and hits player, player takes damage, creates a smoke effect
        if (col.gameObject.layer == 9 && !friendly)
        {
            col.gameObject.GetComponent<Player>().TakeDamage(damage);
            BulletHitEffect newEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            newEffect.GenerateMovement(movingRight);
            Destroy(gameObject);
        }
    }
}
