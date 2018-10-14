using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //Unity editor settings for spawn numbers, waves, and timings
    [SerializeField]
    int initialSpawnNumber = 0;
    [SerializeField]
    int waveSpawnIncrease = 0;
    [SerializeField]
    int waveForHeavies = 0;
    [SerializeField]
    int waveForBombers = 0;
    [SerializeField]
    float timeBetweenSpawns = 0;
    [SerializeField]
    Enemy regEnemy = null;
    [SerializeField]
    Enemy heavyEnemy = null;
    [SerializeField]
    Enemy bombEnemy = null;
    [SerializeField]
    int regWeight = 0;
    [SerializeField]
    int heavyWeight = 0;
    [SerializeField]
    int bombWeight = 0;

    WaveManager waveManager;
    int currentWave;
    int enemiesToSpawn;
    float timer;
    bool gameOver = false;
	
	void Start () {
        //Grabs the wave manager and adds BeginWave() to the delegate
        waveManager = FindObjectOfType<WaveManager>();
        waveManager.startWave += BeginWave;
	}
	
	
	void Update () {
        if (!gameOver)
        {
            timer += Time.deltaTime;

            //Used to space out spawning to prevent one giant clump of enemies
            if (timer >= timeBetweenSpawns && enemiesToSpawn > 0)
            {
                timer -= timeBetweenSpawns;

                //Creates weighting for enemy spawns based on editor settings and what enemies are available
                int rng;

                if (currentWave < waveForHeavies)
                {
                    rng = 0;
                }
                else if (currentWave < waveForBombers)
                {
                    rng = Random.Range(0, regWeight + heavyWeight + 1);
                }
                else
                {
                    rng = Random.Range(0, regWeight + heavyWeight + bombWeight + 1);
                }

                //Spawns enemies based on weighting
                if (rng <= regWeight)
                {
                    Instantiate(regEnemy, transform.position, Quaternion.identity);
                }
                else if (rng <= regWeight + heavyWeight)
                {
                    Instantiate(heavyEnemy, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(bombEnemy, transform.position, Quaternion.identity);
                }

                enemiesToSpawn--;
            }
        }
	}

    void BeginWave(int waveNumber)
    {
        //Updates variables at start of new wave
        timer = 0f;
        currentWave = waveNumber;
        enemiesToSpawn = initialSpawnNumber + (waveNumber * waveSpawnIncrease);
    }

    //Sets game over
    public void GameOver ()
    {
        gameOver = true;
    }
}
