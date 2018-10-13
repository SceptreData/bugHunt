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

        if (!interStarted)
        {
            //If not in intermission, count enemies remaining. Start intermission when no enemies remain
            CountEnemies();
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
        //Will call startWave delegate when spawners are added, causing them to spawn number and types of enemies based on current wave
        //startWave(waveNumber);
    }

    void CountEnemies ()
    {
        //Currently set to 1 for testing
        enemyCount = 1;
        enemyCountText.text = enemyCount.ToString();
    }
}