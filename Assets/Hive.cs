using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour {

    [SerializeField]
    int health = 0;

    bool gameOver = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage (int amount)
    {
        if (!gameOver)
        {
            health -= amount;

            if (health <= 0)
            {
                //Sets game over on all necessary objects
                FindObjectOfType<Player>().GameOver();
                FindObjectOfType<WaveManager>().GameOver();
                Spawner[] spawners = FindObjectsOfType<Spawner>();
                Enemy[] enemies = FindObjectsOfType<Enemy>();

                foreach (Enemy enemy in enemies)
                {
                    enemy.GameOver();
                }

                foreach (Spawner spawner in spawners)
                {
                    spawner.GameOver();
                }

                Destroy(gameObject);
            }
        }
    }

    //Sets game over
    public void GameOver ()
    {
        gameOver = true;
    }
}
