using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed = 0;
    [SerializeField]
    BulletHitEffect hitEffect = null;

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
        //If bullet collides with ground, creates a smoke effect. Initializes it's movement based on facing
        if (col.gameObject.layer == 8)
        {
            BulletHitEffect newEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            newEffect.GenerateMovement(movingRight);
            Destroy(gameObject);
        }
    }
}
