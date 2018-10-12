using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    float speed = 0;

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
}
