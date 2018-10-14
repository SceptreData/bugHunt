using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {


	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        //If player is in range, enemy is set to attacking
        if (col.gameObject.layer == 9)
        {
            GetComponentInParent<Enemy>().SetAttacking(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //If player leaves range, enemy stops attacking. Exception is the bomb enemy, which blows up after a set time
        if (col.gameObject.layer == 9 && !GetComponentInParent<BombEnemy>())
        {
            GetComponentInParent<Enemy>().SetAttacking(false);
        }
    }
}
