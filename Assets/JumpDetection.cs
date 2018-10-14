using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetection : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Tells enemy to jump if there's ground in the way
        if (col.gameObject.layer == 8)
        {
            if (!GetComponentInParent<Enemy>().inAir)
            {
                GetComponentInParent<Enemy>().Jump();
            }
        }
    }
}
