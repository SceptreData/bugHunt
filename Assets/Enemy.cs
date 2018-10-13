using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        //Enemy destroys itself after 5 seconds to test intermission and wave detection
        if (timer > 5f)
        {
            Destroy(gameObject);
        }
	}
}
