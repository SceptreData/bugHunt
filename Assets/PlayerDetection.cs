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
        if (col.gameObject.GetComponent<Player>() || col.gameObject.GetComponent<Hive>())
        {
            GetComponentInParent<Enemy>().SetAttacking(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //If player leaves range, enemy stops attacking. Exception is the bomb enemy, which blows up after a set time
        if (col.gameObject.GetComponent<Player>() || col.gameObject.GetComponent<Hive>())
        {
            //One last check, to make sure there isn't actually still a player/hive in range
            int layerMask = 1 << 9;
            if (Physics2D.Raycast(transform.position, Vector2.left, 7, layerMask))
            {
                //Do nothing, there's still a player/hive in range
            } else if (!GetComponentInParent<BombEnemy>())
            {
                GetComponentInParent<Enemy>().SetAttacking(false);
            }
        }   
    }
}
