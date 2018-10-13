using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

    [SerializeField]
    float maxWaveTime = 0;
    [SerializeField]
    float betweenWaveTime = 0;

    public Text waveNumberText, waveTimerText, enemyCountText, waveState;

    int waveNumber = 0;
    bool interStarted = false;
    bool waveStarted = false;
    float waveTimer = 0;
    int enemyCount = 0;

    public delegate void StartWave(int wave);
    public event StartWave startWave;

    // Use this for initialization
    void Start () {
        BeginIntermission();
	}
	
	// Update is called once per frame
	void Update () {
        waveTimer += Time.deltaTime;

        if (interStarted)
        {
            int remainingTime = Mathf.CeilToInt(betweenWaveTime - waveTimer);
            waveTimerText.text = remainingTime.ToString();

            if (waveTimer >= betweenWaveTime)
            {
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
                waveNumber++;
                BeginWave();
            }
        }

        if (!interStarted)
        {
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
        //startWave(waveNumber);
    }

    void CountEnemies ()
    {
        enemyCount = 1;
        enemyCountText.text = enemyCount.ToString();
    }
}