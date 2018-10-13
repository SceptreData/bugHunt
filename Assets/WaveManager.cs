using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

    [SerializeField]
    float maxWaveTime = 0;
    [SerializeField]
    float betweenWaveTime = 0;

    //Debug UI
    public Text waveNumberText, waveTimerText, enemyCountText, waveState;

    int waveNumber = 0;
    bool interStarted = false;
    bool waveStarted = false;
    float waveTimer = 0;
    float graceTime = 3f;
    int enemyCount = 0;

    public delegate void StartWave(int wave);
    public event StartWave startWave;

    void Start () {
        //Start game with an intermission
        BeginIntermission();
	}
	
	void Update () {
        waveTimer += Time.deltaTime;

        //Checks timer if game is in intermission or a wave
        if (interStarted)
        {
            //Shows remaining time as int
            int remainingTime = Mathf.CeilToInt(betweenWaveTime - waveTimer);
            waveTimerText.text = remainingTime.ToString();

            if (waveTimer >= betweenWaveTime)
            {
                //Starts next wave when time is up
                interStarted = false;
                waveStarted = true;
                BeginWave();
            }
        } else if (waveStarted)
        {
            int remainingTime = Mathf.CeilToInt(maxWaveTime - waveTimer);
            waveTimerText.text = remainingTime.ToString();

            if (waveTimer >= maxWaveTime)
            {
                //Starts next wave when time is up
                waveNumber++;
                BeginWave();
            }
        }

        //Counts remaining enemies
        CountEnemies();

        if (!interStarted && waveTimer >= graceTime)
        {
            //If not in intermission and past grace time to avoid starting next wave immediately, check enemies remaining. Start intermission when no enemies remain
            if (enemyCount == 0)
            {
                BeginIntermission();
            }
        }
	}

    void BeginIntermission ()
    {
        waveStarted = false;
        interStarted = true;
        waveNumber++;
        waveTimer = 0;
        waveNumberText.text = waveNumber.ToString();
        waveState.text = "Intermission";
    }

    void BeginWave ()
    {
        waveStarted = true;
        interStarted = false;
        waveTimer = 0;
        waveNumberText.text = waveNumber.ToString();
        waveState.text = "Wave Started";
        //Calls startWave delegate, causing spawners to spawn number and types of enemies based on current wave
        startWave(waveNumber);
    }

    void CountEnemies ()
    {
        //Grabs all enemy objects, then counts the length for number
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        enemyCount = enemies.Length;
        enemyCountText.text = enemyCount.ToString();
    }
}